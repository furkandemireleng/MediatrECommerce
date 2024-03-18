using System.Reflection;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    // public ApiGatewayDbContext(DbContextOptions<ApiGatewayDbContext> context) : base(context)
    // {
    //     ChangeTracker.LazyLoadingEnabled = false;
    //     //true yaparsan include yapmana gerek kalmaz ama her seyi baglamaya calisir o yuzden false :)
    // }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Detail> Details { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=host.docker.internal;Database=onion;Username=postgres;Port=5435;Password=Bloxima.BLOXIMA.1234");
            // Replace "UseNpgsql" with the appropriate method for your database provider
        }
    }
}