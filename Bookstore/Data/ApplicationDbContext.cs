using System;
using System.Collections.Generic;
using System.Text;
using Bookstore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<BookGenre> BookGenre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Genre>().HasData(
                new Genre()
                {
                    Id = 1,
                    Name = "Fiction"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Non-fiction"
                },
                 new Genre()
                 {
                     Id = 3,
                     Name = "Mystery"
                 },
                 new Genre()
                 {
                     Id = 4,
                     Name = "Romance"
                 },
                 new Genre()
                 {
                     Id = 5,
                     Name = "Comedy"
                 },

                 new Genre()
                 {
                     Id = 6,
                     Name = "Biography"
                 }


                 );
        }
    }
}
