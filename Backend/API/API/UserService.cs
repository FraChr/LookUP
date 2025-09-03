using API.Model;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API;

public class UserService
{

    private readonly string? _connectionString;

    public UserService(ConnectionBuilder connectionBuilder)
    {
        _connectionString = connectionBuilder.GetConnectionString();
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new Exception("ConnectionString not set");
        }
    }

    public async Task CreateUser(User user)
    {
        await using var connection = new SqlConnection(_connectionString);
        var sql = """
                    INSERT INTO Users
                    VALUES(@UserName, @Password, @Email)
                  """;
        await connection.ExecuteAsync(sql, new
        {
            user.UserName,
            user.Password,
            user.Email
        });
    }
}