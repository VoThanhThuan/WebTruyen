using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.UserService
{
    public interface IUserService
    {
        public Task<List<UserVM>> GetUsers();
        public Task<string> Authenticate(LoginRequest request);
        public Task<UserVM> GetUserByAccessTokenAsync(string accessToken);
        public Task<UserVM> GetUser(Guid id);
    }
}
