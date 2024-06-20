using System.Windows.Input;

namespace Presentation.Products;

public sealed record UpdateProductRequest(Guid Id, string Name, string Code, string Description, double Price, int Stock);