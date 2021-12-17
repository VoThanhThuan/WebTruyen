using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Client.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace WebTruyen.UI.Client.Service.UserService
{
    public interface IUserApiClient
    {
        public Task<List<UserAM>> GetUsers();
        public Task<string> Authenticate(LoginRequest request);
        public Task Logout();
        public Task<UserAM> GetUserByAccessTokenAsync(string accessToken);
        public Task<UserAM> GetUser(Guid id);

        public Task<(int statusCode, string mess)> UpdateInfoUser(Guid idUser, InfoUser info);
        public Task<(int statusCode, string mess)> UpdateAvatar(Guid idUser, IBrowserFile avatar);
        public Task<(int statusCode, string mess)> UpdatePassword(Guid idUser, ChangePasswordRequest password);

        public Task<(int apiResult, string mess, UserAM user)> Register(RegisterRequestClient request);

    }
}
