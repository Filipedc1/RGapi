﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RgApi.Shared.Models;

namespace RgApi
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers                          { get; set; }
        public DbSet<Salon> Salons                              { get; set; }
        public DbSet<Product> Products                          { get; set; }
        public DbSet<ProductCollection> ProductCollections      { get; set; }
        public DbSet<Price> Prices                              { get; set; }
        public DbSet<CartItem> ShoppingCartItems                { get; set; }
        public DbSet<State> States                              { get; set; }
        public DbSet<Order> Orders                              { get; set; }
        public DbSet<OrderDetail> OrderDetails                  { get; set; }
        public DbSet<BillingDetail> BillingDetails              { get; set; }
        public DbSet<Comment> Comment                           { get; set; }

        // Junction table
        public DbSet<CollectionProduct> CollectionProducts      { get; set; }


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

            builder.Entity<CollectionProduct>()
                   .HasKey(k => new { k.ProductCollectionId, k.ProductId });
        }
    }
}
