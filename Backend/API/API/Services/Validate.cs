using System.Text.RegularExpressions;
using API.Model.User;
using API.Services.Interfaces;

namespace API.Services;

public partial class Validate : IValidate
{
    [GeneratedRegex(@"^(?=.*\d)(?=.*\p{L})(?=.*\p{Lu})(?=.*[@$!£&%?*~><;#])[\p{L}\d@$!%*?&]{8,}$")]
    private static partial Regex PasswordValidation();
    public void ValidateNewUser(UserDto userDto)
    {
        var result = PasswordValidation().Match(userDto.Password).Success;
        if (!result)
        {
            throw new Exception("Password must be at least 8 characters long include a special character, number and an uppercase letter");
        }

        var equals = userDto.Password.Equals(userDto.PasswordConfirmation);

        if (!equals)
        {
            throw new Exception("Passwords do not match");
        }

        if (userDto.UserName.Length < 5)
        {
            throw new Exception("User name must be 5 or more characters");
        }
    }
}