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
    public string Name { get; set; }
    
    /// <summary>
    /// Code of the product.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Price of the product.
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    /// Description of the product.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Stock of the product.
    /// </summary>
    public int Stock { get; set; }
    
    /// <summary>
    /// Created at date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Updated at date.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Product constructor.
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <param name="name">name of the product</param>
    /// <param name="code">code of the product</param>
    /// <param name="description">description of the product</param>
    /// <param name="price">price of the product</param>
    /// <param name="stock">product available quantity</param>
    /// <param name="createdAt">created at date</param>
    /// <param name="updatedAt">updated at date</param>
    public Product(Guid id, string name, string code, string description, double price, int stock, DateTime createdAt, DateTime? updatedAt = null)
        : base(id)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        Stock = stock;
        CreatedAt = createdAt == default ? DateTime.Now : createdAt;
        UpdatedAt = updatedAt;
    }
    
    public const int NameMaxLength = 256;
    public const int NameMinLength = 3;
    public const int CodeMaxLength = 512;
    public const int CodeMinLength = 3;
    public const int DescriptionMaxLength = 1024;
    public const int DescriptionMinLength = 5;

    public void Update(string requestName, string requestCode, double requestPrice, int requestStock, DateTime requestCreatedAt, string? requestDescription, DateTime? requestUpdatedAt)
    {
        Name = requestName;
        Code = requestCode;
        if(requestDescription != null)
            Description = requestDescription;
        Price = requestPrice;
        Stock = requestStock;
        CreatedAt = requestCreatedAt;
        
        if (requestUpdatedAt != null)
            UpdatedAt = requestUpdatedAt;
    }
}