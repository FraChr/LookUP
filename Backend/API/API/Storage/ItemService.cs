using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class ItemService : ICrudService<Items>
{
    private readonly string _connectionString =
        "Data Source=localhost;Database=LookUp;Integrated Security=true;Connect Timeout=30;Encrypt=true;TrustServerCertificate=true;";

    public Items[] GetAll()
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

        var con = connection.Query<Items>(sql).ToArray();
        return con;
    }

    public void Delete(int itemId)
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "DELETE FROM Items WHERE Id = @LookUpId";
        var rowsAffected = connection.Execute(sql, new { LookUpId = itemId });
    }


}