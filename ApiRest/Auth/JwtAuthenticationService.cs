using entityesLayer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Auth
{
    public class JwtAuthenticationService : IautenticacionService
    {
        private string _key = "e7f31c2c-34c3-4aba-8f12-ce41b022e3b8";

        public JwtAuthenticationService(string key)
        {
            _key = key;
        }

        public string Authenticate(registeredUsers pUsuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim( "Nickname", pUsuario.nickname),
                    new Claim( "Password", pUsuario.userpassword )
                }),
                IssuedAt = DateTime.UtcNow.AddHours(8),
                NotBefore = DateTime.UtcNow.AddMilliseconds(1),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha384Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
