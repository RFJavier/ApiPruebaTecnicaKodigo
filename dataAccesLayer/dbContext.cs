using System;
using System.Collections.Generic;
using System.Text;

//importación paquetes necesarios
using Microsoft.EntityFrameworkCore;
using entityesLayer;


namespace dataAccesLayer
{
    public class dbContext : DbContext
    {
        
        public DbSet<productCategory> productcategory { get; set; }
        public DbSet<products> products { get; set; }
        public DbSet<registeredUsers> registeredusers { get; set; }
        public DbSet<rol> rol { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=RJPC;Initial Catalog=kodigodatabase;Integrated Security=True");
        }
    }
}
