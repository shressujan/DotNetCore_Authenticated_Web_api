using Restapi.Domain;
using Restapi.Models.Request;
using Restapi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Manager
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(UserRequestModel user)
        {
            return await _userRepository.SaveUserToDatabaseAsync(user);
        }

        public async Task<UserModel> GetUser(string email, string password)
        {
            return new UserModel(await _userRepository.GetUserByEmailAndPasswordAsync(email, password));
        }
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(d => new UserModel(d));
        }
    }
}
