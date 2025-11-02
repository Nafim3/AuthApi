using AuthAPI.Entities;
using AuthAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AuthAPI.Services;
using AuthAPI.Data;
using Microsoft.AspNetCore.Authorization;



namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices services;

        public AuthController(IAuthServices serve)
        {
            services = serve;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserInfo>> RegisterUser(UserInfoDTO requserinfo)
        {
            var userInstantiation = await services.RegisterUserAsync(requserinfo);
            if (userInstantiation is null)
            {
                return BadRequest("User already exists");
            }
            return Ok(userInstantiation); // return the created user info
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDto>> LoginUser(UserInfoDTO userInforeq)
        {
            var GenToken = await services.LoginUserAsync(userInforeq);
            if (GenToken is null)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok(GenToken);
        }


        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenReqDto userInforeq)
        {
            var GenToken = await services.RefreshTokenAsync(userInforeq);
            if (GenToken is null)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok(GenToken);
        }


        [HttpGet("AuthorizationEndpoint")]
        [Authorize]
        public ActionResult Authcheck()
        {
            return Ok("You are authorised to access this endpoint");
        }
    }
}
