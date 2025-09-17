using API.Model;
using API.Storage;

namespace API.Services.Interfaces;

public interface ICrudService<TEntity, TCreateDto, TViewModel>
{
    Task<PageResult<TViewModel>> GetAll(int? limit = null, int? page = null);
    Task<PageResult<TViewModel>> Search(string? searchTerm = null, int? limit = null, int? page = null);

    Task<TViewModel> GetById(int id);

    Task Create(TCreateDto dto);

    Task<TViewModel> Update(TCreateDto item, int id);

    Task Delete(int id);
}

public interface ICrudService<TEntity> : ICrudService<TEntity, TEntity, TEntity>
{
}