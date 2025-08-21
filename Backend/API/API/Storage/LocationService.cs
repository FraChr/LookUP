using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class LocationService : ICrudService<Location>
{

    private readonly string _connectionString;
    //     "Data Source=localhost;Database=LookUp;Integrated Security=true;Connect Timeout=30;Encrypt=true;TrustServerCertificate=true;";

    public LocationService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new Exception("Connection string not set");
        }
    }

    // public Location[] GetLocations()
    // {
    //     var connection = new  SqlConnection(_connectionString);
    //     var sql = "SELECT * FROM Room";
    //     var con = connection.Query<Location>(sql).ToArray();
    //     return con;
    // }

    public PageResult<Location> GetAll(int? limit = null, int? page = null)
    {
        var connection = new  SqlConnection(_connectionString);
        var sql = "SELECT * FROM Room";
        var con = connection.Query<Location>(sql).ToArray();
        // return con;
        return new PageResult<Location>
        {
            Data = con,
            Total = con.Count(),
        };
    }

    public void Create(Location item)
    {
        throw new NotImplementedException();
    }


    public void Delete(int itemId)
    {
        throw new NotImplementedException();
    }
}