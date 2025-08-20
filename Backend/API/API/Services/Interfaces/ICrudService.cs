using API.Model;

namespace API.Services.Interfaces;

public interface ICrudService<T>
{
    T[] GetAll();

    void Delete(int itemId);
}