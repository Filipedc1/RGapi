using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RgApi.Models;

namespace RgApi
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetupRelationships(builder);

            //builder.Entity<ProductCollectionProductsJunction>().HasKey(j => new { j.ProductCollectionId, j.ProductId });
        }

        private void SetupRelationships(ModelBuilder builder)
        {
            // each AppUser can have many UserClaims
            builder.Entity<AppUser>()
                   .HasMany(x => x.Claims)
                   .WithOne()
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();

            // each AppUser can have many UserTokens
            builder.Entity<AppUser>()
                   .HasMany(e => e.Tokens)
                   .WithOne()
                   .HasForeignKey(ut => ut.UserId)
                   .IsRequired();
        }
    }
}
