﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Islands.Models.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
