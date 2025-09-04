using API.Model;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class ItemService : ICrudService<Item>
{

    private const int MaxLimit = 1000;
    private readonly AppDbContext _context;



    public ItemService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<PageResult<Item>> GetAll(int? limit = MaxLimit, int? page = null)
    {
        var actualLimit = limit ?? int.MaxValue;
        var offset = ((page ?? 1) - 1) * actualLimit;

        var query = _context.Items
            .Include(i => i.Location)
            .OrderBy(i => i.Id);

        var total = await query.CountAsync();
        var data = await query.Skip(offset).Take(actualLimit).ToListAsync();

        return new PageResult<Item> { Data = data, Total = total };
    }

    public async Task<PageResult<Item>> Search(string? searchTerm = null, int? limit = MaxLimit, int? page = null)
    {
        var actualLimit = limit ?? int.MaxValue;
        var offset = ((page ?? 1) - 1) * actualLimit;

        var query = _context.Items.Include(i => i.Location).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(i => i.Name != null && i.Location.Name != null && (i.Name.Contains(searchTerm) || i.Location.Name.Contains(searchTerm)));
        }
        var total = await query.CountAsync();
        var items = await query
            .OrderBy(i => i.Id)
            .Skip(offset)
            .Take(actualLimit)
            .ToListAsync();

        return new PageResult<Item>
        {
            Data = items,
            Total = total,
        };

    }

    public async Task<Item> GetById(int id)
    {
        var query = _context.Items
            .Include(i => i.Location);

        var result = await query.FirstOrDefaultAsync(i => i.Id == id);

        if (result == null)
        {
            throw new Exception($"Item with ID {id} not found");
        }

        return result;
    }

    public async Task Create(Item item)
    {
        if (item.LocationId == 0)
        {
            throw new Exception("Location not set");
        }

        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<Item> Update(Item item, int itemId)
    {
        var existingItem = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
        if (existingItem == null)
        {
            throw new Exception("Update failed: item not found");
        }

        existingItem.Name = item.Name;
        existingItem.Amount = item.Amount;
        existingItem.LocationId = item.LocationId;

        await _context.SaveChangesAsync();
        return await GetById(itemId);



    }

    public async Task Delete(int itemId)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
        if (item == null)
        {
            throw new Exception("Delete failed: item not found");
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}