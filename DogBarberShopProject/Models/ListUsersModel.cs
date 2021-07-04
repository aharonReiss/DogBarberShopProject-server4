using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Models
{
    public class ListUsersModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime creationQueueTime { get; set; }
        public DateTime queueTime { get; set; }
    }
}
