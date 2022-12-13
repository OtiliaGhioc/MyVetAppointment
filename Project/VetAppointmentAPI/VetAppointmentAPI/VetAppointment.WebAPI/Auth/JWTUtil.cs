using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.Auth
{
    public class JWTUtil
    {
        public JWTUtil(string secretKey, string issuer, string audience, int accessTokenValidityInMinutes, int refreshTokenValidityInDays)
        {
            SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            JWTSigningCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            Issuer = issuer;
            Audience = audience;
            AccessTokenValidityInMinutes = accessTokenValidityInMinutes;
            RefreshTokenValidityInDays = refreshTokenValidityInDays;
        }

        private SymmetricSecurityKey SecretKey { get; set; }
        private SigningCredentials JWTSigningCredentials { get; set; }
        private string Issuer { get; set; }
        private string Audience { get; set; }
        private int AccessTokenValidityInMinutes { get; set; }
        private int RefreshTokenValidityInDays { get; set; }
        private JwtSecurityTokenHandler tokenHandler { get; set; } = new JwtSecurityTokenHandler();
        public Tuple<string?, string?> GetTokensForUser(User user)
        {
            string? accessToken = GenerateToken(user, "access");
            string? refreshToken = GenerateToken(user, "refresh");

            return new Tuple<string?, string?>(accessToken, refreshToken);
        }

        private string? GenerateToken(User user, string type)
        {
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Issuer = Issuer;
            tokenDescriptor.Audience = Audience;
            tokenDescriptor.SigningCredentials = JWTSigningCredentials;

            if (type == "access")
                tokenDescriptor.Expires = DateTime.Now.AddMinutes(AccessTokenValidityInMinutes);
            else if (type == "refresh")
                tokenDescriptor.Expires = DateTime.Now.AddDays(RefreshTokenValidityInDays);
            else
                return null;

            tokenDescriptor.Subject = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.IsMedic ? "medic" : "default"),
                new Claim("token_type", type)
            });

            JwtSecurityToken token = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool IsTokenValid(string token)
        {
            try
            {
                var tokenValidationParams = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(1),
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = SecretKey
                };

                SecurityToken validatedToken;

                var claims = tokenHandler.ValidateToken(token, tokenValidationParams, out validatedToken);
                return true;
            }
            catch (ArgumentException) { return false; }
            catch (SecurityTokenException) { return false; }
        }

        public string? GenerateNewAccessToken(string refreshToken)
        {
            JwtSecurityToken token = (JwtSecurityToken)tokenHandler.ReadToken(refreshToken);
            if (token == null)
                return null;

            Claim? tokenTypeClaim = token.Claims.FirstOrDefault(item => item.Type == "token_type");

            if (tokenTypeClaim == null || tokenTypeClaim.Value != "refresh")
                return null;

            Claim? userIdClaim = token.Claims.FirstOrDefault(item => item.Type == "nameid");

            if (userIdClaim == null)
                return null;
            string userId = userIdClaim.Value;

            Claim? userRoleClaim = token.Claims.FirstOrDefault(item => item.Type == "role");
            string userRole = userIdClaim == null ? "default" : userIdClaim.Value;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Issuer = Issuer;
            tokenDescriptor.Audience = Audience;
            tokenDescriptor.TokenType = "access";
            tokenDescriptor.SigningCredentials = JWTSigningCredentials;
            tokenDescriptor.Expires = DateTime.Now.AddMinutes(AccessTokenValidityInMinutes);

            tokenDescriptor.Subject = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, userRole)
            });

            JwtSecurityToken accessToken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(accessToken);

            return tokenString;
        }
    }
}
