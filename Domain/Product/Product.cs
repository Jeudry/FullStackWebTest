using Domain.Common;

namespace Domain.Product;

/// <summary>
/// Represents a product entity.
/// </summary>
public sealed class Product: Entity
{
    /// <summary>
    /// Name of the product.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Code of the product.
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// Price of the product.
    /// </summary>
    public double Price { get; }
    
    /// <summary>
    /// Description of the product.
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Stock of the product.
    /// </summary>
    public int Stock { get; }

    /// <summary>
    /// Product constructor.
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <param name="name">name of the product</param>
    /// <param name="code">code of the product</param>
    /// <param name="description">description of the product</param>
    /// <param name="price">price of the product</param>
    /// <param name="stock">product available quantity</param>
    public Product(Guid id, string name, string code, string description, double price, int stock)
        : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        Stock = stock;
    }
    
    public const int NameMaxLength = 256;
    public const int NameMinLength = 3;
    public const int CodeMaxLength = 512;
    public const int CodeMinLength = 3;
    public const int DescriptionMaxLength = 1024;
    public const int DescriptionMinLength = 5;
}