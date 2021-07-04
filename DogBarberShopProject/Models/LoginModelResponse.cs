using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Models
{
    public class LoginModelResponse
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
