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
        var sql = "SELECT * FROM Items JOIN Room ON Items.Id = Room.Id";
        // var sql = "SELECT * FROM Items";
        var con = connection.Query<Item>(sql).ToArray();
        return con;
    }
}