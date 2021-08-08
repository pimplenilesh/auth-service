using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AuthenticationService.Service
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetJWTToken(string userId)
        {
            //setting the claims for the user credential name
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Defining security key and encoding the claim 

            var secret = _configuration["Audience:key"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //defining the JWT token essential information and setting its expiration time
            var token = new JwtSecurityToken(
                issuer: _configuration["Audience:Iss"],
                audience: _configuration["Audience:Aud"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: creds
            );
            //defing the response of the token 
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            //convert into the json by serialing the response object
            return JsonConvert.SerializeObject(response);
        }
    }
}
