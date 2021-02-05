using System;
using System.Collections.Generic;
using System.Text;
using Data_eOnlineCarShop.EF_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_eOnlineCarShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne<Administrator>(y => y.Administrator).
                WithOne(x => x.User).HasForeignKey<Administrator>(z => z.UserId);

            modelBuilder.Entity<User>().HasOne<Client>(y => y.Client).
          WithOne(x => x.User).HasForeignKey<Client>(z => z.UserId);

            modelBuilder.Entity<User>().HasOne<Employee>(y => y.Employee).
          WithOne(x => x.User).HasForeignKey<Employee>(z => z.UserId);


        }
        public DbSet<User> User { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Administrator> Administrator { get; set; }

    }
}
