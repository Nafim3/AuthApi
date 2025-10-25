using AuthAPI.Data;
using AuthAPI.Entities;
using AuthAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly AuthDbContext _context;
        public AuthServices(IConfiguration config, AuthDbContext context)
        {
            _configuration = config;
            _context = context; // dependency injection of context
                                // To access appsettings.json and to
                                // automatically pass the app’s configuration
                                // (from appsettings.json, environment variables, etc.) into the controller

        }



        public async Task<ActionResult<UserInfo?>> RegisterUserAsync(UserInfoDTO requserinfo)
        {
            if (await _context.UsersData.AnyAsync(u => u.Username == requserinfo.Username))
            {
                return null;
            }

          

            var userInfo = new UserInfo();

            userInfo.Username = requserinfo.Username;
            userInfo.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(null, requserinfo.Password);
            userInfo.Email = requserinfo.Email;
            await _context.UsersData.AddAsync(userInfo);
            await _context.SaveChangesAsync();

            return userInfo;
        }


        public async Task<string?> LoginUserAsync(UserInfoDTO userInforeq)
        {
            var userInstantiation = await _context.UsersData.FirstOrDefaultAsync(u => u.Username == userInforeq.Username && u.Email == userInforeq.Email);
            if (userInstantiation is null)
            {
                return null;
            }

            if (new PasswordHasher<UserInfo>().VerifyHashedPassword(userInstantiation, userInstantiation.PasswordHash, userInforeq.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }


            string GenToken = GenerateToken(userInstantiation);

            return GenToken;
        }

        private string GenerateToken(UserInfo userInfoTokenReq)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInfoTokenReq.Username ?? string.Empty)
            };

            var TokenBuilder = new JwtSecurityToken
                (
                  issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                  audience: _configuration.GetValue<string>("AppSettings:Audience"),
                  claims: claims,
                  expires: DateTime.Now.AddDays(1),
                  signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(TokenBuilder);

        }



    }
}
