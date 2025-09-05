using API.Model;
using API.Model.User;

namespace API.Services.Interfaces;

public interface IValidate
{
    void ValidateNewUser(UserDto userDto);
}