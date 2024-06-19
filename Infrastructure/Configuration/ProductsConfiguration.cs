using Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class ProductsConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasIndex(product => product.Code).IsUnique();
        builder.Property(product => product.Name).HasMaxLength(256).IsRequired();
        builder.Property(product => product.Code).HasMaxLength(512).IsRequired();
    }
}