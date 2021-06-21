using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Models
{
    public class SalonApiContext: DbContext
    {
        public SalonApiContext(DbContextOptions<SalonApiContext> options) : base(options) { }

        public DbSet<Grooming> Groomings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Veterinar> Veterinars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[] {
                new User{id=1, firstName="Shalom", lastName="Shalom", username="admin", password="nimda"}
            });

            modelBuilder.Entity<Veterinar>().HasData(new Veterinar[] {
                new Veterinar{id=1, firstName="Stanley", lastName="M.McRoy"},
                new Veterinar{id=2, firstName="David", lastName="Juarez"},
                new Veterinar{id=3, firstName="Louis", lastName="Mike Starson"},
            });
        }
    }
}
