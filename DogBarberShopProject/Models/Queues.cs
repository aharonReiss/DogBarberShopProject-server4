using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Models
{
    public class Queues
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime CreationQueueTime { get; set; }
        public DateTime QueueTime { get; set; }

    }
}
