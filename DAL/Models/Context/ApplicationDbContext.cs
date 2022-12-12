using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public List<Notification> Notifications { get; set; }

        public List<User> Users { get; set; }

        public List<Player> Players { get; set; }

        public List<Advertisement> Advertisements{ get; set; }

        public List<Building> Buildings { get; set; }
    }
}
