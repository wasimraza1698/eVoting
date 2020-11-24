﻿using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
        public IActionResult Get(User user)
        {
            User valUser = _userRepo.Get(user);
            if (valUser == null)
            {
                return NotFound();
            }

            return Ok(valUser);
        }

        [HttpPost("Login")]
        public IActionResult Login(User user)
        {

            try
            {
                _log4net.Info("Authentication initiated for UserId " + user.UserID.ToString());
                if (ModelState.IsValid)
                {
                    IActionResult response;
                    User valUser = _userRepo.Get(user);
                    if (valUser == null)
                    {
                        _log4net.Info("User not found");
                        return NotFound();
                    }
                    else
                    {
                        _log4net.Info("Logging in user with id "+valUser.UserID.ToString());
                        var tokenString = GenerateJSONWebToken(valUser);
                        response = Ok(new { token = tokenString });
                        return response;
                    }
                }
                return BadRequest();
            }
            catch
            {
                return new NoContentResult();
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

