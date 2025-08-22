using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace API.Storage;

public class ItemService : ICrudService<Items>
{
    private readonly string _connectionString;
        // "Data Source=localhost;Database=LookUp;Integrated Security=true;Connect Timeout=30;Encrypt=true;TrustServerCertificate=true;";

    public ItemService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new Exception("Connection string not set");
        }
    }


    public PageResult<Items> GetAll(int? limit = null, int? page = null)
    {
        var connection = new  SqlConnection(_connectionString);
        int actualLimit = limit ?? int.MaxValue;
        int offset = ((page ?? 1) - 1) * actualLimit;
        var sql = """
                  SELECT Items.Id,
                         Items.Name,
                         items.Amount,
                         Room.Name AS Location
                  FROM Items 
                  JOIN Room 
                  ON Items.Location = Room.Id
                  ORDER BY Items.id
                  OFFSET @Offset ROWS
                  FETCH NEXT @Limit ROWS ONLY
                  """;

        var totalCount = "SELECT COUNT(*) FROM Items";

        var con = connection.Query<Items>(sql, new {Offset = offset, Limit = actualLimit}).ToArray();
        var total = connection.ExecuteScalar<int>(totalCount);
        // return con;
        return new PageResult<Items>
        {
            Data = con,
            Total = total,
        };
    }

    public void Create(Items item)
    {
        if (item.Location.IsNullOrEmpty())
        {
            throw new Exception("Location not set");
            // throw new BadHttpRequestException("Location not set");
        }

        var connection = new  SqlConnection(_connectionString);
        var sql = """
                    INSERT INTO Items (Name, Amount, Location)
                    VALUES (@Name, @Amount, @Location)
                  """;
        connection.Execute(sql, new
        {
            item.Name,
            item.Amount,
            item.Location
        });
    }

    public void Delete(int itemId)
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "DELETE FROM Items WHERE Id = @LookUpId";
        var rowsAffected = connection.Execute(sql, new { LookUpId = itemId });
    }


}