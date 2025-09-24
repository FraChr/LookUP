using API.Data;
using API.Model;
using API.Model.User;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCryptHasher = BCrypt.Net.BCrypt;

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

    public async Task<UserViewModel> GetById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
        };
    }

    public async Task Create(UserDto userDto)
    {
        try
        {
            _validate.ValidateNewUser(userDto);

            var exists = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email.ToLower() || u.Username == userDto.Username.ToLower());
            if (exists)
            {
                throw new Exception("User already exists");
            }

            var passwordHash = BCryptHasher.HashPassword(userDto.Password);

            var user = new User
            {
                Username = userDto.Username.ToLower(),
                Email = userDto.Email.ToLower(),
                HashedPassword = passwordHash,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<UserViewModel> Update(UserDto dto, int id)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        existingUser.Username = dto.Username;
        existingUser.Email = dto.Email;

        await _context.SaveChangesAsync();
        return await GetById(id);
    }

    public async Task Delete(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if(user == null) { throw new Exception("User not found"); }

        var items = await _context.Items.Where(i => i.UserId == id).ToListAsync();
        var shelfs = await _context.Shelfs.Where(s => s.UserId == id).ToListAsync();
        var rooms = await _context.Room.Where(r => r.UserId == id).ToListAsync();

        _context.Items.RemoveRange(items);
        _context.Shelfs.RemoveRange(shelfs);
        _context.Room.RemoveRange(rooms);
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        Console.WriteLine($"USER: {user.Username} ITEMS: {items.Count}");
    }
}