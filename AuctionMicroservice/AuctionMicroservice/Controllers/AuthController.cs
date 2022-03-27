using AuctionMicroservice.Data;
using AuctionMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuctionMicroservice.Controllers
{
    [ApiController]
    [Route("api/auth/login")]


    public class AuthController : ControllerBase
    {
        private readonly IUserRegistrationRepository repository;

        public AuthController(IUserRegistrationRepository repository)
        {
            this.repository = repository;
        }

         [HttpPost]
       
        //[EnableCors("EnableCORS")]
        public  IActionResult Login([FromBody] Login user)
        {
            if(user == null)
            {
                return BadRequest("Invalid client request");
            }

            var entity = repository.GetRegistrationByEmail(user.UserName);

            if (user.UserName == "lukap181@gmail.com")
            {

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Admin")

                };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@123"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:40003",
                    audience: "http://localhost:40003",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                //HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(new { Token = tokenString });
            }

            if (user.UserName == entity.Email && user.Password == entity.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@123"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:40003",
                    audience: "http://localhost:40003",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                //HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(new { Token = tokenString }); 
            }

            

            return Unauthorized(); 
        }

        
    }
}
