using API.Data;
using API.Model;
using API.Model.Shelf;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class ShelfsService : ICrudService<Shelfs, ShelfsDto, ShelfsViewModel>
{

    private readonly AppDbContext _context;

    public ShelfsService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<PageResult<ShelfsViewModel>> GetAll(int? limit = null, int? page = null)
    {
        var query = await _context.Shelfs.ToListAsync();
        var result = query.Select(q => new ShelfsViewModel
        {
            Id = q.Id,
            Name = q.Name,
        });
        return new PageResult<ShelfsViewModel>
        {
            Data = result,
            Total = query.Count(),
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

    public Task Create(ShelfsDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ShelfsViewModel> Update(ShelfsDto item, int id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}