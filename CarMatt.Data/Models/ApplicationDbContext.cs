using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace CarMatt.Data.Models
{


    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            });


            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            });


            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.HasMany(c => c.Images).WithOne(e => e.Vehicle).IsRequired();
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");

            });

        }

    }


}
