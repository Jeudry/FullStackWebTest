namespace TestCommon.TestsConstants;

internal static partial class Constants
{
    
    public static class Product
    {
        public const string Text = "Fang";
        public const string Description = "A fang to bite";
        public const double Price = 100.0;
        public const int Stock = 10;
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly DateTime CreatedAt = DateTime.UtcNow;
        public static readonly DateTime? UpdatedAt = DateTime.UtcNow;
        public static int Limit = 10;
        public static int Offset = 0;
        public const string SortBy = "name";
        public const string Direction = "asc";
    }
}