using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Entities
{
    public class UserInfo
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        
        [Key]
        public int UserId { get; set; }
        public string? Email { get; set; }
    }
}
