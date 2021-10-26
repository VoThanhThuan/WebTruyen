using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.User
{
    public interface IUserService
    {
        public Task<IEnumerable<UserAM>> GetUsers(int skip = 0, int take = 20);
        public Task<string> Authenticate(LoginRequest request);
        public Task<UserAM> GetUserFromAccessToken(string accessToken);
        public Task<UserAM> GetUser(Guid id);
        public Task<bool> PutUser(Guid id, UserRequest request);
        public Task<(int apiResult, string mess, UserAM user)> PostUser(UserRequest request);
        public Task<(int apiResult, string mess, UserAM user)> Register(UserRequest request);
        public Task<int> DeleteUser(Guid id);

    }
}
