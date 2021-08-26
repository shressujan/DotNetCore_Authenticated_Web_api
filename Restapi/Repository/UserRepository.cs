using Dapper;
using Restapi.Domain;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public UserRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var query = "SELECT * FROM user";
            using (var connection = _unitOfWork.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var param = new DynamicParameters();
            param.Add("@email", email);
            param.Add("@password", password);

            var query = @"SELECT * from user WHERE email=@email AND password=@password";

            using (var connection = _unitOfWork.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, param);
                return user;
            }
        }

        public async Task<bool> SaveUserToDatabaseAsync(UserRequestModel user)
        {
            var param = new DynamicParameters();
            param.Add("@username", user.Username);
            param.Add("@email", user.Email);
            param.Add("@password", user.Password);

            var query = @"INSERT INTO user(username, email, password) VALUES (@username, @email, @password)";

            using (var connection = _unitOfWork.CreateConnection())
            {
                await connection.ExecuteAsync(query, param);
                return true;
            }
        }
    }
}
