using API.Data;
using API.Model;
using API.Model.Items;
using API.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class ItemService : ICrudService<Item, ItemDto, ItemViewModel>
{

    private const int MaxLimit = 1000;
    private readonly AppDbContext _context;
    private readonly IUserContextService _userContext;



    public ItemService(AppDbContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContext = userContextService;
    }


    public async Task<PageResult<ItemViewModel>> GetAll(int? limit = MaxLimit, int? page = null)
    {
        var userId = _userContext.GetUserId();
        // var actualLimit = limit ?? int.MaxValue;
        // var offset = ((page ?? 1) - 1) * actualLimit;

        var offset = Paging.CalculateOffset(page, limit);

        var query = _context.Items
            .Where(i => i.UserId == userId )
            .Include(i => i.Location)
            .Include(i => i.Shelfs)
            .OrderBy(i => i.Id);

        var total = await query.CountAsync();
        var items = await query.Skip(offset).Take(Paging.GetActualLimit(limit)).ToListAsync();

        var viewModels = items .Select(item => new ItemViewModel
        {
            Id = item.Id,
            Name = item?.Name ?? "Unknown",
            Amount = item.Amount,
            Location = item.Location?.Name ?? "Unknown",
            LocationId = item.LocationId,
            Shelf = item.Shelfs?.Name ?? "Unknown",
            ShelfsId = item.ShelfsId,
            Timestamp = item.Timestamp
        });

        return new PageResult<ItemViewModel> { Data = viewModels, Total = total };
    }

    public async Task<PageResult<ItemViewModel>> Search(string? searchTerm = null, int? limit = MaxLimit, int? page = null)
    {
        var userId = _userContext.GetUserId();
        var actualLimit = limit ?? int.MaxValue;
        var offset = ((page ?? 1) - 1) * actualLimit;

        var query = _context.Items
            .Include(i => i.Location)
            .Include(i => i.Shelfs)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(i => i.Name != null && i.Location.Name != null && (i.Name.Contains(searchTerm) || i.Location.Name.Contains(searchTerm)));

        }
        var total = await query.CountAsync();
        var items = await query
            .Where(i => i.UserId == userId )
            .OrderBy(i => i.Id)
            .Skip(offset)
            .Take(actualLimit)
            .ToListAsync();

        var viewModels = items.Select(item => new ItemViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Amount = item.Amount,
            Location = item.Location?.Name ?? "Unknown",
            Shelf = item.Shelfs?.Name ?? "Unknown",
            Timestamp = item.Timestamp
        });

        return new PageResult<ItemViewModel>
        {
            Data = viewModels,
            Total = total,
        };

    }

    public async Task<ItemViewModel> GetById(int id)
    {
        var item = await _context.Items
            .Include(i => i.Location)
            .Include(item => item.Shelfs)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            throw new Exception($"Item with ID {id} not found");
        }

        return new ItemViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Amount = item.Amount,
            Location = item.Location?.Name ?? "Unknown",
            LocationId = item.LocationId,
            Shelf = item.Shelfs?.Name ?? "Unknown",
            ShelfsId = item.ShelfsId,
            Timestamp = item.Timestamp
        };
    }

    public async Task Create(ItemDto dto)
    {

        var userId = _userContext.GetUserId();

        Console.WriteLine($"Create called with: Name={dto.Name}, Amount={dto.Amount}, LocationId={dto.LocationId}");
        if (dto.LocationId == 0)
        {
            throw new Exception("Location not set");
        }

        var item = new Item
        {
            Name = dto.Name,
            Amount = dto.Amount,
            LocationId = dto.LocationId,
            UserId = userId,
            ShelfsId = dto.ShelfsId,
            Timestamp = DateOnly.FromDateTime(DateTime.Now)
        };

        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<ItemViewModel> Update(ItemDto dto, int id)
    {
        var userId = _userContext.GetUserId();
        var existingItem = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        if (existingItem == null)
        {
            throw new Exception("Update failed: item not found");
        }

        existingItem.Name = dto.Name;
        existingItem.Amount = dto.Amount;
        existingItem.LocationId = dto.LocationId;
        existingItem.ShelfsId = dto.ShelfsId;

        await _context.SaveChangesAsync();
        return await GetById(id);
    }

    public async Task Delete(int id)
    {
        var userId = _userContext.GetUserId();
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        if (item == null)
        {
            throw new Exception("Delete failed: item not found");
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}