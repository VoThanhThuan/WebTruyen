using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities;

namespace WebTruyen.API.Repository.RoleDI
{
    public interface IRoleService
    {
        public Task<IEnumerable<Library.Entities.Role>> GetRoles();
        public Task<Library.Entities.Role> GetRole(Guid id);
        public Task<bool> PutRole(Guid id, Library.Entities.Role request);
        public Task<bool> PostRole(Library.Entities.Role request);
        public Task<bool> DeleteRole(Guid id);

    }
}
