using API.Model;
using API.Storage;

namespace API.Services.Interfaces;

public interface ICrudService<T>
{
    Task<PageResult<T>> GetAll(int? limit = null, int? page = null);
    Task<PageResult<T>> Search(string? searchTerm = null, int? limit = null, int? page = null);

    Task<T> GetById(int id);

    Task Create(T item);

    Task<T> Update(T item, int id);

    Task Delete(int itemId);
}