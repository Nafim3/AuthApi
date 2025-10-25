using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Model
{
    public class UserInfoDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
