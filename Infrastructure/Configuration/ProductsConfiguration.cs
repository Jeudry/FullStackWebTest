using Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class ProductsConfiguration: IEntityTypeConfiguration<Product>
{
    private const string ProductId = "dc043262-673a-491a-b811-446703743743";
    private const string ProductId2 = "dc043262-673a-491a-b811-446703743744";
    private const string ProductId3 = "dc043262-673a-491a-b811-446703743745";
    private const string ProductId4 = "dc043262-673a-491a-b811-446703743746";
    private const string ProductId5 = "dc043262-673a-491a-b811-446703743747";
    private const string ProductId6 = "dc043262-673a-491a-b811-446703743748";
    private const string ProductId7 = "dc043262-673a-491a-b811-446703743749";
    private const string ProductId8 = "dc043262-673a-491a-b811-446703743750";
    private const string ProductId9 = "dc043262-673a-491a-b811-446703743751";
    private const string ProductId10 = "dc043262-673a-491a-b811-446703743752";
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasIndex(product => product.Name).IsUnique();
        builder.Property(product => product.Name).HasMaxLength(Product.NameMaxLength).IsRequired();
        builder.Property(product => product.Description).HasMaxLength(Product.DescriptionMaxLength);
        builder.Property(product => product.Price).IsRequired();
        builder.Property(product => product.Stock).IsRequired();
        
        builder.HasData(
            new Product(
                Guid.Parse(ProductId),
                "Chair",
                "Its a chair to sit",
                100,
                10,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId2),
                "Table",
                "Its a table to put things on",
                200,
                20,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId3),
                "Sofa",
                "Its a sofa to sit",
                300,
                30,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId4),
                "Bed",
                "Its a bed to sleep",
                400,
                40,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId5),
                "Lamp",
                "Its a lamp to light",
                500,
                50,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId6),
                "Curtains",
                "Its a curtain to cover",
                600,
                60,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId7),
                "Carpet",
                "Its a carpet to walk",
                700,
                70,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId8),
                "Painting",
                "Its a painting to see",
                800,
                80,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId9),
                "Mirror",
                "Its a mirror to reflect",
                900,
                90,
                DateTime.UtcNow
            )
        );
        builder.HasData(
            new Product(
                Guid.Parse(ProductId10),
                "Vase",
                "Its a vase to hold",
                1000,
                100,
                DateTime.UtcNow
            )
        );
    }
}