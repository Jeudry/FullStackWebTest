namespace TestCommon.TestsConstants;

internal static partial class Constants
{
    public static class User
    {
        public const string UserName = "TestUser";
        public const string Email = "the_jeudry@hotmail.com";
        public const string Password = "Admin@1234";
        public const string ConfirmPassword = "Admin@1234";

        public static readonly Guid Id = Guid.NewGuid();
    }
}