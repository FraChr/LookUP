using API.Model;
using API.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class LocationService : ICrudService<Location>
{
    private readonly string _connectionString;
    private readonly AppDbContext _context;

    public LocationService(ConnectionBuilder connectionBuilder, AppDbContext context)
    {

        _context = context;

        _connectionString = connectionBuilder.GetConnectionString();
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new Exception("Connection string not set");
        }
    }

    public async Task<PageResult<Location>> GetAll(int? limit = null, int? page = null)
    {
        var query = await _context.Room.ToListAsync();

        return new PageResult<Location>
        {
            Data = query,
            Total = query.Count(),
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