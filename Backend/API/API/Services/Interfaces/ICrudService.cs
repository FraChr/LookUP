using API.Model;
using API.Storage;

namespace API.Services.Interfaces;

public interface ICrudService<T>
{
    PageResult<T> GetAll(int? limit = null, int? page = null);

    void Create(T item);

    void Delete(int itemId);
}