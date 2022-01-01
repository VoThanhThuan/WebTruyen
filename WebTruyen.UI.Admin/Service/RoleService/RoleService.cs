using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities;

namespace WebTruyen.UI.Admin.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _http;

        public RoleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Role>> GetRoles()
        {
            var result = await _http.GetFromJsonAsync<List<Role>>("/api/Roles");
            return result;
        }

        public Task<Role> GetRole(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutRole(Guid id, Role request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostRole(Role request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRole(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
