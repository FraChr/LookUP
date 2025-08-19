using API.Model;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class ItemStorage
{
    private readonly string _connectionString =
        "Data Source=localhost;Database=LookUp;Integrated Security=true;Connect Timeout=30;Encrypt=true;TrustServerCertificate=true;";

    public Item[] GetItems()
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = """
                  SELECT Items.Id,
                         Items.Name,
                         items.Amount,
                         Room.Name AS Location
                  FROM Items 
                      JOIN Room 
                  ON Items.Location = Room.Id
                  """;

        var con = connection.Query<Item>(sql).ToArray();
        return con;
    }

    public void DeleteItem(int itemId)
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "DELETE FROM Items WHERE Id = @LookUpId";
        var rowsAffected = connection.Execute(sql, new { LookUpId = itemId });
    }

    public Location[] getLocations()
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "SELECT * FROM Room";
        var con = connection.Query<Location>(sql).ToArray();
        return con;
    }
}