using API.Data;
using API.Model;
using API.Model.User;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Storage;

public class UserService : ICrudService<User, UserDto, UserViewModel>
{

    private readonly AppDbContext _context;
    private readonly IValidate _validate;

    public UserService(IValidate validate, AppDbContext context)
    {
        _context = context;
        _validate = validate;
    }


    public Task<PageResult<UserViewModel>> GetAll(int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<UserViewModel>> Search(string? searchTerm = null, int? limit = null, int? page = null)
    {
        throw new NotImplementedException();
    }

    public Task<UserViewModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Create(UserDto userDto)
    {
        try
        {
            _validate.ValidateNewUser(userDto);

            var exists = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email || u.UserName == userDto.UserName);
            if (exists)
            {
                throw new Exception("User already exists");
            }


            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                HashedPassword = userDto.Password,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<UserViewModel> Update(UserDto item, int id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}