using API.Model;
using API.Storage;

namespace API.Services.Interfaces;

public interface ICrudService<T>
{
    PageResult<T> GetAll(int? limit = null, int? page = null);
    PageResult<T> Search(string? searchTerm = null, int? limit = null, int? page = null);

    T GetById(int id);

    void Create(T item);

    T Update(T item, int id);

    void Delete(int itemId);
}