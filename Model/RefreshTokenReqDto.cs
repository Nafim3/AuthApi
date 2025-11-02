namespace AuthAPI.Model
{
    public class RefreshTokenReqDto
    {
        public string? RefreshToken { get; set; }
        public int? UserId { get; set; }
    }
}
