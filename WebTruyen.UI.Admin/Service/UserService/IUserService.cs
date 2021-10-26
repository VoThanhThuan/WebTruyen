using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Admin.RequestClient;

namespace WebTruyen.UI.Admin.Service.UserService
{
    public interface IUserService
    {
        public Task<List<UserAM>> GetUsers();
        public Task<string> Authenticate(LoginRequest request);
        public Task<UserAM> GetUserByAccessTokenAsync(string accessToken);
        public Task<UserAM> GetUser(Guid id);
        public Task<int> PutUser(Guid id, UserRequestClient request);
        public Task<(HttpStatusCode StatusCode, UserAM)> PostUser(UserRequestClient request);
        public Task<int> DeleteUser(Guid id);
    }
}
