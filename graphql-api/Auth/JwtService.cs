using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GraphQL_Api.Auth;

{
    public static class JwtService
    {
        public static string Issue(string email, string secret, int days = 7)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(
                subject: new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(days));
            return handler.WriteToken(token);
        }
    }
}
