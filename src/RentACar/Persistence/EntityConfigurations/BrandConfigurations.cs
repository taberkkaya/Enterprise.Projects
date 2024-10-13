using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BrandConfigurations : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.Id).HasColumnName("Name").IsRequired();
        builder.Property(b => b.Id).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.Id).HasColumnName("UpdatedDate");
        builder.Property(b => b.Id).HasColumnName("DeletetedDate");

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}
