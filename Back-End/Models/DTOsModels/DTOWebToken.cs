namespace Back_End.Models.DTOsModels
{
    public class DTOWebToken
    {
        public string Token { get; set; } = null!;
        public DateTime ExpirationToken { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpirationRefreshToken { get; set; }

    }
}
