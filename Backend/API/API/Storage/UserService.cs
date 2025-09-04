using API.Model;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class UserService : ICrudService<User>
{

    private readonly AppDbContext _context;
    private readonly IValidate _validate;

    public UserService(IValidate validate, AppDbContext context)
    {
        _context = context;
        _validate = validate;
    }

    public Task<PageResult<User>> GetAll(int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<User>> Search(string? searchTerm = null, int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Create(User user)
    {
        _validate.ValidateUser(user);

        var exists = await _context.Users
            .AnyAsync(u => u.Email == user.Email || u.UserName == user.UserName);
        if (exists)
        {
            throw new Exception("User already exists");
        }
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public Task<User> Update(User item, int id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int itemId)
    {
        throw new NotImplementedException();
    }
}