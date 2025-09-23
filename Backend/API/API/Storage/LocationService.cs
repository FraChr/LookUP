using API.Data;
using API.Model;
using API.Model.Location;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class LocationService : ICrudService<Location, LocationDto, LocationViewModel>
{
    private readonly AppDbContext _context;
    private readonly IUserContextService _userContext;

    public LocationService(AppDbContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContext = userContextService;
    }

    public async Task<PageResult<LocationViewModel>> GetAll(int? limit = null, int? page = null)
    {
        var userId = _userContext.GetUserId();


        var query = await _context.Room.Where(q => q.UserId == userId).ToListAsync();

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
        var userId = _userContext.GetUserId();

        var room = new Location
        {
            Name = dto.Name,
            UserId = userId
        };

        await _context.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    public async Task<LocationViewModel> Update(LocationDto dto, int id)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int locationId)
    {
        var userId = _userContext.GetUserId();
        var room = await _context.Room.FirstOrDefaultAsync(q => q.Id == locationId && q.UserId == userId);
        if (room == null)
        {
            throw new Exception("Delete failed: Room not found");
        }
        _context.Room.Remove(room);
        await _context.SaveChangesAsync();
    }
}