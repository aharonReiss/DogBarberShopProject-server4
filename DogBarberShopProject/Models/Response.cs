using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Models
{
    public class Response<T>
    {
        public string Status { get; set; }
        public T Result { get; set; }
    }
}
