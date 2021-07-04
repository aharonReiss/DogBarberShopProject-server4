using DogBarberShopProject.Interfaces;
using DogBarberShopProject.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopProject.Services
{
    public class AuthenticationService : IAuthentication
    {
        private DataContext context;
        string key = "my_secret_dogBarberShopProject";

        public AuthenticationService(DataContext dc)
        {
            context = dc;
        }

        public void AddUser(User user)
        {
            user.Password = this.HashPassword(user.Password);
            context.users.Add(user);
            context.SaveChanges();
        }

        public string CreateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string DecodeToken(string token)
        {   
            var handler = new JwtSecurityTokenHandler();
            var tok = handler.ReadJwtToken(token);
            var t = tok.Claims.Where(c => c.Type == "unique_name")
                .Select(u => u.Value).SingleOrDefault();
            return t;
        }

        public string HashPassword(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public bool IsUserExist(string username)
        {
            var userInUse = context.users.SingleOrDefault(u => u.UserName == username);
            if (userInUse != null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(LoginModel user)
        {
            var userInDb = context.users.SingleOrDefault(u => u.UserName == user.UserName);
            if (userInDb != null && BCrypt.Net.BCrypt.Verify(user.Password, userInDb.Password))
            {
                return true;
            }
            return false;
        }
    }
}
