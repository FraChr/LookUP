using API.Model;
using API.Model.Items;
using API.Model.Location;
using API.Model.User;
using API.Services;

namespace API.Extensions;

public static class EndpointExtensions
{
    public static void MapStorageEndpoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var mapper = scope.ServiceProvider.GetRequiredService<EndpointMapperService>();

        mapper.MapEndpoints<Item, ItemDto, ItemViewModel>(app, "storage");
        mapper.MapEndpoints<Location, LocationDto, LocationViewModel>(app, "location");
        mapper.MapEndpoints<User, UserDto, UserViewModel>(app, "user");
        mapper.MapAuthEndpoints(app);
    }
}