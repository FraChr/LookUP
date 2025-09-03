using API.Model;

namespace API.Services.Interfaces;

public interface IValidate
{
    void ValidateUser(User user);
}