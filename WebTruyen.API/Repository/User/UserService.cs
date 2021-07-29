using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public UserService(ComicDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<IEnumerable<UserVM>> GetUsers()
        {
            return await _context.Users.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<UserVM> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            return user?.ToViewModel();
        }

        public async Task<bool> PutUser(Guid id, UserRequest request)
        {
            var text = new TextService();
            var user = await _context.Users.FindAsync(request.Id);
            
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

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return false;
                throw;
            }

            return true;
        }

        public async Task<bool> PostUser(UserRequest request)
        {
            var user = request.ToUser();
            user.Avatar = await SaveFile(request.Avatar);
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
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
    }
}
