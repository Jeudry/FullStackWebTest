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
        builder.Property(product => product.Name).HasMaxLength(Product.NameMaxLength).IsRequired();
        builder.Property(product => product.Code).HasMaxLength(Product.CodeMaxLength).IsRequired();
        builder.Property(product => product.Description).HasMaxLength(Product.DescriptionMaxLength);
        builder.Property(product => product.Price).IsRequired();
        builder.Property(product => product.Stock).IsRequired();
    }
}