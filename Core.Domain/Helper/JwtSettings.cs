namespace Infrastructure.Identity
{
    public static class JwtSettings
    {
        public static string  Issuer = "https://localhost:7017/";
        public static string Audience = "https://localhost:7017/";
        public static string Key = "TrTrWmtTtwUezw131252333ErmiaERMIAermia098098098098";
        public static bool ValidateIssuer { get; set; }
        public static bool ValidateAudience { get; set; }
        public static bool ValidateLifeTime { get; set; }
        public static bool ValidateIssuerSigningKey { get; set; }
        public static int AccessTokenExpireDate { get; set; }
        public static int RefreshTokenExpireDate { get; set; }
    }
}
