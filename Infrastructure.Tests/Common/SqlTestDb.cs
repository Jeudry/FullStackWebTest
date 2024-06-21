using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Tests.Common;

public class SqlTestDb: IDisposable
{

    private const string ConnectionStrings = "ConnectionStrings";
    private const string SqlConnectionString = "SqlServerConnection";
    private const string DbConnectionString = "DB_CONNECTION_STRING";
    
    public SqlConnection Connection { get; }
    
    public static SqlTestDb CreateAndInitialize()
    {
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString =
            configuration.GetSection(ConnectionStrings)[SqlConnectionString];
        
        configuration["ApplyMigrations"] = "false"; 

        var testDatabase = new SqlTestDb(connectionString);

        testDatabase.InitializeDatabase();

        return testDatabase;
    }
    
    public void InitializeDatabase()
    {
         Connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Connection.ConnectionString)
            .Options;
        

        using var context = new AppDbContext(options, null!, null!);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
    
    public void ResetDatabase()
    {
        Connection.Close();

        InitializeDatabase();
    }
    
    private SqlTestDb(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
    }
    
    public void Dispose()
    {
        Connection.Close();
    }
}