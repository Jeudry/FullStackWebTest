using Domain.Common;

namespace Domain.Products;

/// <summary>
/// Represents a product entity.
/// </summary>
public sealed class Product: Entity
{
    public string Name { get; }
    
    /// <summary>
    /// Product constructor.
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <param name="name">name of the product</param>
    public Product(Guid id, string name)
        : base(id)
    {
        Name = name;
    }
}