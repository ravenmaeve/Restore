using System;
using API.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
        .HasData(
            new IdentityRole { Id = "f100f198-706a-4c30-b69c-f17b5b96ae64", Name = "Member", NormalizedName = "MEMBER" },
            new IdentityRole { Id = "982e1573-3024-41be-8161-08929f211810", Name = "Admin", NormalizedName = "ADMIN" }
        );
    }
    
}
