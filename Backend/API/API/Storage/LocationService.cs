using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Storage;

public class LocationService : ICrudService<Location>
{
    private readonly string _connectionString;

    public LocationService(ConnectionBuilder connectionBuilder)
    {
        _connectionString = connectionBuilder.GetConnectionString();
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new Exception("Connection string not set");
        }
    }

    public async Task<PageResult<Location>> GetAll(int? limit = null, int? page = null)
    {
        await using var connection = new  SqlConnection(_connectionString);
        const string sql = "SELECT * FROM Room";
        var con = (await connection.QueryAsync<Location>(sql)).ToArray();

        return new PageResult<Location>
        {
            Data = con,
            Total = con.Length,
        };
    }

    public async Task<PageResult<Location>> Search(string? searchTerm = null, int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Location> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Create(Location item)
    {
        throw new NotImplementedException();
    }

    public async Task<Location> Update(Location item, int id)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int itemId)
    {
        throw new NotImplementedException();
    }
}