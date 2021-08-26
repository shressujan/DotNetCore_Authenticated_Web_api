using Restapi.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Models.Request
{
    public class UserModel
    {
        private readonly User _entity;
        public UserModel(User entity)
        {
            _entity = entity;
        }

        [Required]
        public string Username => _entity.Username;
        [Required]
        public string Email => _entity.Email;
        [Required]
        public string Password => _entity.Password;

    }
}
