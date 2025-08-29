using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class ItemService : ICrudService<Items>
{
    private readonly string? _connectionString;
    private const int MaxLimit = 1000;



    public ItemService(ConnectionBuilder connectionBuilder)
    {
        _connectionString = connectionBuilder.GetConnectionString();
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new Exception("Connection string not set");
        }
    }


    public PageResult<Items> GetAll(int? limit = MaxLimit, int? page = null)
    {
        using var connection = new  SqlConnection(_connectionString);
        Console.WriteLine($"CONNECTION STRING {_connectionString}");
        var actualLimit = limit ?? int.MaxValue;
        var offset = ((page ?? 1) - 1) * actualLimit;
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

    public PageResult<Items> Search(string? searchTerm = null, int? limit = MaxLimit, int? page = null)
    {
        using var connection = new  SqlConnection(_connectionString);

        var parameters = new DynamicParameters();
        var whereClause = "";

        var actualLimit = limit ?? int.MaxValue;
        var offset = ((page ?? 1) - 1) * actualLimit;

        parameters.Add("Limit", actualLimit);
        parameters.Add("Offset", offset);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            whereClause = "WHERE Items.Name LIKE @SearchTerm OR Room.Name LIKE @SearchTerm";
            parameters.Add("SearchTerm", $"%{searchTerm}%");
        }


        var sql = $"""
                  SELECT Items.Id,
                         Items.Name,
                         Items.Amount,
                         Room.Name AS Location,
                         Room.Id AS LocationId
                  FROM Items
                  JOIN Room ON Items.LocationId = Room.Id
                  {whereClause}
                  ORDER BY Items.id
                  OFFSET @Offset ROWS
                  FETCH NEXT @Limit ROWS ONLY
                  """;

        var totalCount = $"""
                          SELECT COUNT(*) FROM Items
                          JOIN Room ON Items.LocationId = Room.Id
                          {whereClause}
                          """;

        var con = connection.Query<Items>(sql, parameters).ToArray();
        var total = connection.ExecuteScalar<int>(totalCount, parameters);

        return new PageResult<Items>
        {
            Data = con,
            Total = total,
        };
    }

    public Items GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
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
        using var connection = new SqlConnection(_connectionString);
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
        using var connection = new  SqlConnection(_connectionString);
        var sql = "DELETE FROM Items WHERE Id = @LookUpId";
        var rowsAffected = connection.Execute(sql, new { LookUpId = itemId });
    }


}