using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Common;

public class SqlTestDb: IDisposable
{
    public SqlConnection Connection { get; }
    
    public static SqlTestDb CreateAndInitialize()
    {
        var testDatabase = new SqlTestDb("Data Source=.; Initial Catalog=FullStackDb; User=sa; Password=Wepsys@123; ApplicationIntent=ReadWrite; MultiSubnetFailover=False; Trusted_Connection=False; Encrypt=True; TrustServerCertificate=True; Connection Timeout=30;");

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