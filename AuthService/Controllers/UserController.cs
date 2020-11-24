using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserController));
        public readonly IUserRepo _userRepo;
        private readonly IConfiguration _config;
        public UserController(IUserRepo userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }
        [HttpPost("Get")]
        public User Get([FromBody]User valUser)
        {
            _log4net.Info("Trying to get User");
            return _userRepo.Get(valUser);
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]User login)
        {

            _log4net.Info("Authentication initiated for UserId " + login.UserID.ToString());
            IActionResult response = Unauthorized();
            User user = Get(login);
            if (user == null)
            {
                _log4net.Info("User not found");
                return NotFound();
            }
            else
            {
                _log4net.Info("Logging in");
                var tokenString = GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
                return response;
            }
        }
        private string GenerateJSONWebToken(User user)
        {
            _log4net.Info("Token Generation initiated for UserId " + user.UserID.ToString());
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jet:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }
    }


}

