using API.Data;
using API.Model;
using API.Model.Shelf;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class ShelfsService : ICrudService<Shelfs, ShelfsDto, ShelfsViewModel>
{

    private readonly AppDbContext _context;
    private readonly IUserContextService _userContext;

    public ShelfsService(AppDbContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContext = userContextService;
    }


    public async Task<PageResult<ShelfsViewModel>> GetAll(int? limit = null, int? page = null)
    {
        var userId = _userContext.GetUserId();

        var offset = Paging.CalculateOffset(page, limit);

        var query = _context.Shelfs.Where(q => q.UserId == userId);

        var total = await query.CountAsync();
        var shelfs = await query.Skip(offset).Take(Paging.GetActualLimit(limit)).ToListAsync();


        var result = shelfs.Select(shelf => new ShelfsViewModel
        {
            Id = shelf.Id,
            Name = shelf.Name,
        });
        return new PageResult<ShelfsViewModel>
        {
            Data = result,
            Total = total,
        };
    }

    public Task<PageResult<ShelfsViewModel>> Search(string? searchTerm = null, int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<ShelfsViewModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Create(ShelfsDto dto)
    {
        var userId = _userContext.GetUserId();
        var shelf = new Shelfs
        {
            Name = dto.Name,
            UserId = userId
        };

        await _context.AddAsync(shelf);
        await _context.SaveChangesAsync();
    }

    public Task<ShelfsViewModel> Update(ShelfsDto item, int id)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        var userId = _userContext.GetUserId();
        var shelf = _context.Shelfs.FirstOrDefault(q => q.Id == id && q.UserId == userId);
        if (shelf == null)
        {
            throw new Exception("Delete failed: Shelf not found");
        }
        _context.Shelfs.Remove(shelf);
        await _context.SaveChangesAsync();
    }
}