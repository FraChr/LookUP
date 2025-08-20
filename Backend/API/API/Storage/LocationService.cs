using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class LocationService : ICrudService<Location>
{

    private readonly string _connectionString =
        "Data Source=localhost;Database=LookUp;Integrated Security=true;Connect Timeout=30;Encrypt=true;TrustServerCertificate=true;";
    // public Location[] GetLocations()
    // {
    //     var connection = new  SqlConnection(_connectionString);
    //     var sql = "SELECT * FROM Room";
    //     var con = connection.Query<Location>(sql).ToArray();
    //     return con;
    // }

    public Location[] GetAll()
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "SELECT * FROM Room";
        var con = connection.Query<Location>(sql).ToArray();
        return con;
    }

    public void Delete(int itemId)
    {
        throw new NotImplementedException();
    }
}