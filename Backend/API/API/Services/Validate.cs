using System.Text.RegularExpressions;
using API.Model;
using API.Services.Interfaces;

namespace API.Services;

public partial class Validate : IValidate
{
    [GeneratedRegex(@"^(?=.*\d)(?=.*\p{L})(?=.*\p{Lu})(?=.*[@$!£&%?*~><;#])[\p{L}\d@$!%*?&]{8,}$")]
    private static partial Regex PasswordValidation();
    public void ValidateUser(User user)
    {
        var result = PasswordValidation().Match(user.Password).Success;
        if (!result)
        {
            throw new Exception("Password must be at least 8 characters long include a special character, number and an uppercase letter");
        }
    }
}