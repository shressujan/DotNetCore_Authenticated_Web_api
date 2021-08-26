using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restapi.Manager;
using Restapi.Models.Request;
using Restapi.Models.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthManagementController: ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(IUserManager userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequestModel user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.GetUser(user.Email, user.Password);

                if(existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exist"
                        }
                    });
                }

                var isCreated = await _userManager.CreateUser(user);

                if(isCreated)
                {
                    var jwtToken = GenerateJwtToken(user);

                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }

            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Invalid Payload"
                }
            });
        }

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn([FromQuery]string email, [FromQuery]string password)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.GetUser(email, password);
                var existingUserModel = new UserRequestModel
                {
                    Username = existingUser.Username,
                    Email = existingUser.Email
                };

                if(existingUser != null)
                {
                    var jwtToken = GenerateJwtToken(existingUserModel);

                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Invalid Payload"
                }
            });
        }

        private string GenerateJwtToken(UserRequestModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

    }
}
