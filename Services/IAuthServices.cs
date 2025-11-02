using AuthAPI.Entities;
using AuthAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services
{
    public interface IAuthServices
    {
        Task<TokenResponseDto?> LoginUserAsync(UserInfoDTO userInforeq);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenReqDto userInforeq);
        Task<ActionResult<UserInfo?>> RegisterUserAsync(UserInfoDTO requserinfo);
    }
}