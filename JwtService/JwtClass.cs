using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Xml.Linq;

namespace UI_API.JwtService
{
    public class JwtClass
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }

        private readonly IConfiguration config;

        public JwtClass(IConfiguration _config) 
        {
            config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string id, string Name, string Email, string createdOn) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("id",id),
                new Claim("Name", Name),
                new Claim("Email", Email),
                new Claim("createdOn", createdOn)
            };

            var jwtToken = new JwtSecurityToken(
                    
                    issuer :"localhost",
                    audience : "localhost",
                    claims : payload,
                    expires: DateTime.Now.AddMinutes(TokenDuration),
                    signingCredentials : signature
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
