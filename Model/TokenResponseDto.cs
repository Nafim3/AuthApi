namespace AuthAPI.Model
{
    public class TokenResponseDto
    {
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
