namespace LoanSystem.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string? Issuer { get; set; }
        public string? IssuerSigningKey { get; set; }
        public TimeSpan Expiry { get; set; }
        public string? Audience { get; set; }
    }
}
