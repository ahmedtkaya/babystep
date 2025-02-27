using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using babystepV1.Models;

namespace babystepV1.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Kids> Kids { get; set; }
        public DbSet<Diaper> Diapers { get; set; }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();  // Email alanı benzersiz olacak
    }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<User>().HasMany(u => u.Kids)
        //     .WithOne(k => k.User)
        //     .HasForeignKey(k => k.UserId)
        //     .OnDelete(DeleteBehavior.Cascade);
        // }
    }
}