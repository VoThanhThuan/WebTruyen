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
            var user = request.ToUser();
            if(request.Avatar != null)
                user.Avatar = await SaveFile(request.Avatar);
            if(!string.IsNullOrEmpty(request.Password))
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Entry(request.ToUser()).State = EntityState.Modified;

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

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().Value;
            var fileName = $@"avatar/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
