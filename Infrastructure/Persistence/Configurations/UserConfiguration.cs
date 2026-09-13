using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.TelegramId)
            .IsUnique();

        builder.Property(user => user.TelegramId)
            .IsRequired();

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.HasMany(user => user.SearchRequests)
            .WithOne(searchRequest => searchRequest.User)
            .HasForeignKey(searchRequest => searchRequest.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}