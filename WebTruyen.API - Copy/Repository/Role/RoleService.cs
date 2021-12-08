using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;

namespace WebTruyen.API.Repository.Role
{
    public class RoleService : IRoleService
    {
        private readonly ComicDbContext _context;

        public RoleService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Library.Entities.Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Library.Entities.Role> GetRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);

            return role ?? null;
        }

        public async Task<bool> PutRole(Guid id, Library.Entities.Role request)
        {
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> PostRole(Library.Entities.Role request)
        {
            _context.Roles.Add(request);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return false;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return true;
        }
        private bool RoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }

    }
}
