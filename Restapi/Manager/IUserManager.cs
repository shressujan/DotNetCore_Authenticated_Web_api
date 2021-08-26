using Restapi.Domain;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Manager
{
    public interface IUserManager
    {
        public Task<bool> CreateUser(UserRequestModel user);
        public Task<UserModel> GetUser(string email, string password);
        public Task<IEnumerable<UserModel>> GetUsers();
    }
}
