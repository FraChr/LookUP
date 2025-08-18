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
}