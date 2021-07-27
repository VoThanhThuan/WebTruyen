using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.User
{
    public interface IUserService
    {
        public Task<IEnumerable<UserVM>> GetUsers();
        public Task<UserVM> GetUser(Guid id);
        public Task<bool> PutUser(Guid id, UserVM request);
        public Task<bool> PostUser(UserVM request);
        public Task<bool> DeleteUser(Guid id);

    }
}
