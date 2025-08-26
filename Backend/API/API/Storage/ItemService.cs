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
                         Room.Name AS Location,
                         Room.Id AS LocationId
                  FROM Items 
                  JOIN Room 
                  ON Items.LocationId = Room.Id
                  ORDER BY Items.id
                  OFFSET @Offset ROWS
                  FETCH NEXT @Limit ROWS ONLY
                  """;

        var totalCount = "SELECT COUNT(*) FROM Items";

        var con = connection.Query<Items>(sql, new {Offset = offset, Limit = actualLimit}).ToArray();
        var total = connection.ExecuteScalar<int>(totalCount);

        return new PageResult<Items>
        {
            Data = con,
            Total = total,
        };
    }

    public Items GetById(int id)
    {
        var connection = new SqlConnection(_connectionString);
        var sql = """
                  SELECT
                      
                  	   Items.Name,
                  	   Items.Amount,
                  	   Room.Id AS LocationId,
                  	   Room.Name AS Location
                  	   
                  FROM Items
                  JOIN Room
                  ON Items.LocationId = Room.Id
                  WHERE Items.Id = @Id
                  """;
        var con = connection.QueryFirstOrDefault<Items>(sql, new { Id = id });
        return con;
    }

    public void Create(Items item)
    {
        if (item.LocationId == 0)
        {
            throw new Exception("Location not set");
            // throw new BadHttpRequestException("Location not set");
        }

        var connection = new  SqlConnection(_connectionString);
        var sql = """
                    INSERT INTO Items (Name, Amount, LocationId)
                    VALUES (@Name, @Amount, @LocationId)
                  """;
        connection.Execute(sql, new
        {
            item.Name,
            item.Amount,
            item.LocationId
        });
    }

    public Items Update(Items item, int itemId)
    {
        // int roomId;
        // if (!Enum.TryParse<Rooms>(item.Location, out var room))
        // {
        //     throw new Exception("Location not set");
        // }

        var connection = new SqlConnection(_connectionString);
        var sql = """
                  UPDATE Items
                  SET 
                  Name = @Name,
                  Amount = @Amount,
                  LocationId = @LocationId
                  WHERE Id = @Id
                  """;


        var parameters = new
        {
            Name = item.Name,
            Amount = item.Amount,
            LocationId = item.LocationId,
            Id = itemId
        };

        int rowsAffected = connection.Execute(sql, parameters);

        if(rowsAffected == 0)
        {
            Console.WriteLine("Update failed: No rows updated. ID may not exist");
            throw new Exception("Update failed: item not found");
        };

        return GetById(itemId);
    }

    public void Delete(int itemId)
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "DELETE FROM Items WHERE Id = @LookUpId";
        var rowsAffected = connection.Execute(sql, new { LookUpId = itemId });
    }


}