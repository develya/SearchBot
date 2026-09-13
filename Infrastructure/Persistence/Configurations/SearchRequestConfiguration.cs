using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SearchRequestConfiguration  : IEntityTypeConfiguration<SearchRequest>
{
    public void Configure(EntityTypeBuilder<SearchRequest> builder)
    {
        builder.HasKey(searchRequest => searchRequest.Id);

        builder.Property(searchRequest => searchRequest.CityId)
            .IsRequired();

        builder.HasIndex(searchRequest => searchRequest.UserId);
    }
}