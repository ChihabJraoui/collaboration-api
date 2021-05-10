using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Collaboration.ShareDocs.Application.Commands.Authentication.Dto;
using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Collaboration.ShareDocs.Application.Common.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.UsersRefreshTokens = new Dictionary<string, string>();
        }

        public IDictionary<string, string> UsersRefreshTokens { get; set; }

        public string GenerateRandomToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public object Authenticate(string userId, Claim[] claims)
        {
            var expiryInMinutesString = this._configuration["Jwt:ExpiryInMinutes"];
            var signingKey = this._configuration["Jwt:SigningKey"];
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var expiryInMinutes = Convert.ToInt32(expiryInMinutesString);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                                      claims),

                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = this.GenerateRandomToken();
            if (this.UsersRefreshTokens.ContainsKey(userId))
            {
                this.UsersRefreshTokens[userId] = refreshToken;
            }
            else
            {
                this.UsersRefreshTokens.Add(userId, refreshToken);
            }

            var response = new LoginDto(  );
            response.RefreshToken = refreshToken;
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return response;
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> listClaims            = new List<Claim>();
            var         tokenHandler          = new JwtSecurityTokenHandler();
            var         expiryInMinutesString = this._configuration["Jwt:ExpiryInMinutes"];
            var         signingKey            = this._configuration["Jwt:SigningKey"];
            listClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //Todo use RSA encryption instead
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var expiryInMinutes = Convert.ToInt32(expiryInMinutesString);

            var claims = new ClaimsIdentity(listClaims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
