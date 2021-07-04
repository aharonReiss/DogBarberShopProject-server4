using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Models
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> opts)
           : base(opts) { }
        public DbSet<User> users { get; set; }
        public DbSet<Queues> queues { get; set; }

    }
}
