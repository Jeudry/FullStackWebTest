using Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class ProductsConfiguration: IEntityTypeConfiguration<Product>
{
    private const string ProductId = "dc043262-673a-491a-b811-446703743743";
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
        
        builder.HasData(
            new Product(
                Guid.Parse(ProductId),
                "Product",
                "PROD",
                "Product description",
                100,
                10,
                DateTime.UtcNow
            )
        );
    }
}