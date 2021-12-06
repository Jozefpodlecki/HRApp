using HRApp.Common;
using HRApp.DAL.Repositories;
using HRApp.Web.Configuration;
using HRApp.Web.Models;
using HRApp.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HRApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IOptionsMonitor<JwtConfiguration> _config;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtBuilder _jwtBuilder;

        public AuthController(
            IOptionsMonitor<JwtConfiguration> config,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtBuilder jwtBuilder)
        {
            _config = config;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtBuilder = jwtBuilder;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CreateToken([FromBody] Login model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError(".", Strings.InvalidUserOrPassword);
                return ValidationProblem();
            }

            var passwordHash = _passwordHasher.ComputeHash(model.Password, user.PasswordHashSalt);

            if(!_passwordHasher.Compare(passwordHash, user.PasswordHash))
            {
                ModelState.AddModelError(".", Strings.InvalidUserOrPassword);
                return ValidationProblem();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim(nameof(IUserContext.UserId), user.Id.ToString()),
            };

            var tokenExpiry = _config.CurrentValue.TokenExpiry;

            var tokenData = _jwtBuilder.Build(tokenExpiry, claims);
            return Ok(tokenData);
        }

       
    }
}
