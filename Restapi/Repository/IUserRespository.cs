using Restapi.Domain;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<bool> SaveUserToDatabaseAsync(UserRequestModel user);
    }
}
