namespace Infrastructure.Tests.Common;

[CollectionDefinition(CollectionName)]
public class WebAppFactoryCollection: ICollectionFixture<WebAppFactory>
{
    public const string CollectionName = "WebAppFactoryCollection";
}