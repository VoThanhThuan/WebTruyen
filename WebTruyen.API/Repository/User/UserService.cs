﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.User
{
    public class UserService : IUserService
    {
        private readonly ComicDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IPasswordHasher<Library.Entities.User> _passwordHasher = new PasswordHasher<Library.Entities.User>();
        private readonly UserManager<Library.Entities.User> _userManager; //Thư viện quản lý user
        private readonly RoleManager<Library.Entities.Role> _roleManager; //Thư viện quản lý role
        private readonly SignInManager<Library.Entities.User> _signInManager; //Thư viên đăng nhập
        private readonly IConfiguration _config; //lấy config từ appsetting.config

        public UserService(ComicDbContext context, 
            IStorageService storageService, 
            SignInManager<Library.Entities.User> signInManager, 
            UserManager<Library.Entities.User> userManager,
            RoleManager<Library.Entities.Role> roleManager,
            IConfiguration config)
        {
            _context = context;
            _storageService = storageService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<IEnumerable<UserVM>> GetUsers(int skip = 0, int take = 20)
        {
            var users = await _context.Users.Skip(skip).Take(take).Select(x => x).ToListAsync();
            var uservm = new List<UserVM>();
            foreach (var user in users)
            {
                uservm.Add(await user.ToViewModel(_userManager));
            }
            return uservm;
        }

        public async Task<UserVM> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return await user.ToViewModel(_userManager);
        }

        public async Task<bool> PutUser(Guid id, UserRequest request)
        {
            var text = new TextService();
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return false;
            user.Nickname = string.IsNullOrEmpty(text.RemoveSpaces(request.Nickname)) == true ? user.Nickname : request.Nickname;
            user.Dob = request.Dob ?? user.Dob;
            user.sex = request.sex ?? user.sex;
            user.Address = string.IsNullOrEmpty(text.RemoveSpaces(request.Address)) == true ? user.Address : request.Address;
            user.Fanpage = string.IsNullOrEmpty(text.RemoveSpaces(request.Fanpage)) == true ? user.Fanpage : request.Fanpage;
            user.Email = string.IsNullOrEmpty(text.RemoveSpaces(request.Email)) == true ? user.Email : request.Email;
            user.PhoneNumber = string.IsNullOrEmpty(text.RemoveSpaces(request.PhoneNumber)) == true ? user.PhoneNumber : request.PhoneNumber;
            user.UserName = string.IsNullOrEmpty(text.RemoveSpaces(request.Username)) == true ? user.UserName : request.Username;

            if (!string.IsNullOrEmpty(text.RemoveSpaces(request.Password)))
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            if (request.Avatar != null)
                user.Avatar = await SaveFile(request.Avatar);

            //_context.Entry(request.ToUser()).State = EntityState.Modified;

            //Role assign

            var roles = await _context.Roles.Select(x => x).ToListAsync();
            var userRole = await _context.AppUserRole.FindAsync(id, request.IdRole);
            if (userRole == null)
            {
                foreach (var role in roles)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name) == true)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }

                var roleNew = await _context.Roles.FindAsync(request.IdRole);

                await _userManager.AddToRoleAsync(user, roleNew.Name);
            }

            try
            {
                await _userManager.UpdateAsync(user);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return false;
                throw;
            }

            return true;
        }

        public async Task<(int apiResult, string mess, UserVM user)> PostUser(UserRequest request)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);
            if (checkUser != null)
                return (StatusCodes.Status409Conflict, "Username đã tồn tại", null);
            var user = request.ToUser();
            user.Id = Guid.NewGuid();
            user.Avatar = await SaveFile(request.Avatar);
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return (StatusCodes.Status409Conflict, "Tạo tài khoản thất bại", null);
            }

            //Role assign
            var role = await _context.Roles.FindAsync(request.IdRole);
            if (request.IdRole == Guid.Empty || role == null)
            {
                role = await _roleManager.FindByNameAsync("Guest");
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            return (StatusCodes.Status200OK, "Ok",user.ToViewModel());
        }

        public async Task<(int apiResult, string mess, UserVM user)> Register(UserRequest request)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);
            if (checkUser != null)
                return (StatusCodes.Status409Conflict, "Username đã tồn tại", null);
            var user = request.ToUser();
            user.Id = Guid.NewGuid();
            user.Avatar = await SaveFile(request.Avatar);
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return (StatusCodes.Status409Conflict, "Tạo tài khoản thất bại", null);
            }

            //Role assign
            var role = await _roleManager.FindByNameAsync("Guest");

            await _userManager.AddToRoleAsync(user, role.Name);

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            return (StatusCodes.Status200OK, "Ok", user.ToViewModel());
        }

        public async Task<int> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return StatusCodes.Status404NotFound;
            }

            var result = await DeleteFile(user.Avatar);
            if (result != 200)
                return StatusCodes.Status500InternalServerError;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();


            return StatusCodes.Status200OK;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            return  await _storageService.SaveFile(file, @"avatar/");
        }
        private async Task<int> DeleteFile(string fileName)
        {
            return await _storageService.DeleteFileAsync(fileName);
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user); //lấy quyền người dùng
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, string.IsNullOrEmpty(user.Email) ? "" : user.Email),
                new Claim(ClaimTypes.GivenName, user.Nickname),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            //<>Mã hóa SymmetricS
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //<> Tạo Token
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            //</>

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string DecryptString(string cipherText)
        {
            //<>Mã hóa SymmetricS
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return "";
        }

        public async Task<UserVM> GetUserFromAccessToken(string accessToken)
        {

            var token = accessToken;
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            return null;
        }

    }
}
