using AuthAPI.Entities;
using AuthAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services
{
    public interface IAuthServices
    {
        Task<string?> LoginUserAsync(UserInfoDTO userInforeq);
        Task<ActionResult<UserInfo?>> RegisterUserAsync(UserInfoDTO requserinfo);
    }
}