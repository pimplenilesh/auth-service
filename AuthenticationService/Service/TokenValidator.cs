using AuthenticationService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Service
{
    public class TokenValidator : ITokenValidator
    {
        private readonly IConfiguration configuration;
        public TokenValidator(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public bool ValidateToken(AuthToken token)
        {
            var audienceconfig = configuration.GetSection("Audience");
            var key = audienceconfig["key"];
            var keyByteArray = Encoding.ASCII.GetBytes(key);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParamters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = audienceconfig["Iss"],

                ValidateAudience = true,
                ValidAudience = audienceconfig["Aud"],

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };


            var jwtHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;

            bool isTokenValid = false;

            try
            {
               var claimPrinciple = jwtHandler.ValidateToken(token.Token, tokenValidationParamters, out validatedToken);
                if(claimPrinciple.Claims != null)
                {
                    isTokenValid = true;
                }
            }
            catch (Exception ex)
            {
                isTokenValid = false;
            }
            
            return isTokenValid;
        }
    }
}
