using Microsoft.Data.SqlClient;
using DotNetEnv;

namespace API;

public class ConnectionBuilder
{
    // private readonly IConfiguration _configuration;

    private readonly string _connectionString;

    public ConnectionBuilder(IConfiguration configuration)
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = configuration["Database:Server"],
            InitialCatalog = configuration["Database:Database"],
            UserID = Environment.GetEnvironmentVariable("DB_USER"),
            Password = Environment.GetEnvironmentVariable("DB_PASSWORD"),
            Encrypt = true,
            TrustServerCertificate = true
        };

        _connectionString = builder.ConnectionString;
    }

    public string GetConnectionString()
    {
        return _connectionString;
    }

}