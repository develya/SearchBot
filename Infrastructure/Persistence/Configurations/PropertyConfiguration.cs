using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(property => property.Id);
        
        builder.Property(property => property.ExternalId)
            .IsRequired();

        builder.HasIndex(property => property.ExternalId)
            .IsUnique();
        
        builder.Property(property => property.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(property => property.Description)
            .HasMaxLength(5000);
        
        builder.Property(property => property.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(property => property.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(property => property.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(property => property.Address)
            .HasMaxLength(300);

        builder.Property(property => property.Url)
            .IsRequired()
            .HasMaxLength(1000);
    }
}