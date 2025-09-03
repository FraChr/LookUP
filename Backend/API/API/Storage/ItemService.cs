using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class ItemService : ICrudService<Item>
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


    public async Task<PageResult<Item>> GetAll(int? limit = MaxLimit, int? page = null)
    {
        await using var connection = new  SqlConnection(_connectionString);
        var actualLimit = limit ?? int.MaxValue;
        var offset = ((page ?? 1) - 1) * actualLimit;
        var sql = """
                  SELECT Items.Id,
                         Items.Name,
                         items.Amount,
                         Items.LocationId,
                         Room.Id AS Id,
                         Room.Name AS Name
                  FROM Items 
                  JOIN Room ON Items.LocationId = Room.Id
                  ORDER BY Items.id
                  OFFSET @Offset ROWS
                  FETCH NEXT @Limit ROWS ONLY
                  """;

        var totalCount = "SELECT COUNT(*) FROM Items";

        var items = (await connection.QueryAsync<Item, Location, Item>(
            sql,
            (item, location) =>
            {
                item.Location = location;
                return item;
            },
            new {Offset = offset, Limit = actualLimit},
            splitOn: "Id"
        )).ToArray();

        // var con = (await connection.QueryAsync<Item>(sql, new { Offset = offset, Limit = actualLimit })).ToArray();
        var total = await connection.ExecuteScalarAsync<int>(totalCount);

        return new PageResult<Item>
        {
            Data = items,
            Total = total,
        };
    }

    public async Task<PageResult<Item>> Search(string? searchTerm = null, int? limit = MaxLimit, int? page = null)
    {
        await using var connection = new  SqlConnection(_connectionString);

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
                         Room.Id AS Id,
                         Room.Name AS Name
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

        var items = (await connection.QueryAsync<Item, Location, Item>(
            sql,
            (item, location) =>
            {
                item.Location = location;
                return item;
            },
            parameters,
            splitOn: "Id"
            )).ToArray();

        // var con = (await connection.QueryAsync<Item>(sql, parameters)).ToArray();
        var total = await connection.ExecuteScalarAsync<int>(totalCount, parameters);

        return new PageResult<Item>
        {
            Data = items,
            Total = total,
        };
    }

    public async Task<Item> GetById(int id)
    {
        await using var connection = new SqlConnection(_connectionString);
        var sql = """
                  SELECT
                       Items.Id,
                  	   Items.Name,
                  	   Items.Amount,
                  	   Items.LocationId,
                  	   Room.Id AS Id,
                  	   Room.Name AS Name
                  FROM Items
                  JOIN Room
                  ON Items.LocationId = Room.Id
                  WHERE Items.Id = @Id
                  """;

        var result = await connection.QueryAsync<Item, Location, Item>(
            sql,
            (item, location) =>
            {
                item.Location = location;
                return item;
            },
            new { Id = id },
            splitOn: "Id"
        );

        var item = result.FirstOrDefault();

        if (item == null)
        {
            throw new Exception($"Item with ID {id} not found");
        }
        return item;

        // var con = await connection.QueryFirstOrDefaultAsync<Item>(sql, new { Id = id });
        // return con;
    }

    public async Task Create(Item item)
    {
        if (item.LocationId == 0)
        {
            throw new Exception("Location not set");
        }

        await using var connection = new  SqlConnection(_connectionString);
        var sql = """
                    INSERT INTO Items (Name, Amount, LocationId)
                    VALUES (@Name, @Amount, @LocationId)
                  """;
        await connection.ExecuteAsync(sql, new
        {
            item.Name,
            item.Amount,
            item.LocationId
        });
    }

    public async Task<Item> Update(Item item, int itemId)
    {
        await using var connection = new SqlConnection(_connectionString);
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

        var rowsAffected = await connection.ExecuteAsync(sql, parameters);

        if(rowsAffected == 0)
        {
            throw new Exception("Update failed: item not found");
        };

        return await GetById(itemId);
    }

    public async Task Delete(int itemId)
    {
        await using var connection = new  SqlConnection(_connectionString);
        var sql = "DELETE FROM Items WHERE Id = @LookUpId";
        var rowsAffected = await connection.ExecuteAsync(sql, new { LookUpId = itemId });
    }


}