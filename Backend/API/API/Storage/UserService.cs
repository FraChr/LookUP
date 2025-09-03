using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class UserService : ICrudService<User>
{

    private readonly string? _connectionString;
    private readonly IValidate _validate;

    public UserService(ConnectionBuilder connectionBuilder, IValidate validate)
    {
        _validate = validate;
        _connectionString = connectionBuilder.GetConnectionString();
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new Exception("ConnectionString not set");
        }
    }

    // public async Task CreateUser(User user)
    // {
    //
    // }

    public Task<PageResult<User>> GetAll(int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<User>> Search(string? searchTerm = null, int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Create(User user)
    {

        _validate.ValidateUser(user);

        await using var connection = new SqlConnection(_connectionString);

        var result = CheckUsernameEmail(user);

        if (result != null)
        {
            throw new Exception("User already exists");
        }



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

    private async Task<bool> CheckUsernameEmail(User user)
    {
        await using var connection = new SqlConnection(_connectionString);
        var userExist = """
                            SELECT TOP 1 UserName, Email 
                            FROM Users 
                            WHERE UserName = @UserName 
                            OR Email = @Email 
                            
                        """;

        var result = await connection.QueryFirstOrDefaultAsync(userExist, new
        {
            user.UserName,
            user.Email
        });

        return result == null;
    }

    public Task<User> Update(User item, int id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int itemId)
    {
        throw new NotImplementedException();
    }
}