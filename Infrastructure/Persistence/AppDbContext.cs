using Domain;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Property = Domain.Property;
using PropertyConfiguration = Microsoft.EntityFrameworkCore.Metadata.Internal.PropertyConfiguration;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<SearchRequest> SearchRequests { get; set; }

    public DbSet<Property> Properties { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}