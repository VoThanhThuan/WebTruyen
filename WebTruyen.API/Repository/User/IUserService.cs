using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.User
{
    public interface IUserService
    {
        public Task<IEnumerable<UserVM>> GetUsers();
        public Task<UserVM> GetUser(Guid id);
        public Task<bool> PutUser(Guid id, UserRequest request);
        public Task<bool> PostUser(UserRequest request);
        public Task<int> DeleteUser(Guid id);

    }
}
