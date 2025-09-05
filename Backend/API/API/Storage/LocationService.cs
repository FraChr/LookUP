using API.Data;
using API.Model;
using API.Model.Location;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class LocationService : ICrudService<Location, LocationDto, LocationViewModel>
{
    private readonly AppDbContext _context;

    public LocationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<LocationViewModel>> GetAll(int? limit = null, int? page = null)
    {
        var query = await _context.Room.ToListAsync();

        var viewModels = query.Select(q => new LocationViewModel
        {
            Id = q.Id,
            Name = q.Name,
        });

        return new PageResult<LocationViewModel>
        {
            Data = viewModels,
            Total = query.Count(),
        };
    }

    public async Task<PageResult<LocationViewModel>> Search(string? searchTerm = null, int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public async Task<LocationViewModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Create(LocationDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<LocationViewModel> Update(LocationDto dto, int id)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int itemId)
    {
        throw new NotImplementedException();
    }
}