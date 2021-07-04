using DogBarberShopProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Interfaces
{
    public interface IAuthentication
    {
        bool IsValidPassword(string password);
        bool VerifyPassword(LoginModel user);
        string HashPassword(string password);
        bool IsUserExist(string username);
        void AddUser(User user);
        string CreateToken(string username);
        string DecodeToken(string token);
    }
}
