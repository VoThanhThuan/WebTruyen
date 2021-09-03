using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities;

namespace WebTruyen.UI.Admin.Service.RoleService
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<Role> GetRole(Guid id);
        public Task<bool> PutRole(Guid id, Role request);
        public Task<bool> PostRole(Role request);
        public Task<bool> DeleteRole(Guid id);
    }
}
