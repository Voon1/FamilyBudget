using Autofac.Core;
using FamilyBudget.Api.Interface;
using FamilyBudget.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FamilyBudget.Api.Controllers
{
    //[ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController //: BaseController<FamilyBudgetController>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
    
        
        public AuthenticationController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(nameof(Auth))]
        public string Auth([FromBody] User data)
        {
            //someday DTO
            User foundUser =  _userService.UserGet(data.UserName, data.UserPassword);
            bool isValid = _userService.IsValidUser(foundUser);
            if (isValid)
            {
                var tokenString = GenerateJwtToken(data.UserName);

                //For swagger to not rewrite Bearer all the time
                return "Bearer " + tokenString;
            }
            return "Invalid credentials";
        }

        /// <summary>
        /// Simple method just to check if User is validated.
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(TestAuthorisation))]
        public string TestAuthorisation()
        {
            return "API Validated";
        }
        /// <summary>
        /// Generate JWTToken
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyToken = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyToken), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UserInsert", Name = "UserInsert")]
        public async Task<int> UserInsert(string userName, string userPassword)
        {
            //TODO VALIDATION!


            return await _userService.UserInsert(userName, userPassword);
        }
    }
}
