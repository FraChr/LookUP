using API.Model;
using API.Services;

namespace API.Extensions;

public static class EndpointExtensions
{
    public static void MapStorageEndpoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var mapper = scope.ServiceProvider.GetRequiredService<EndpointMapperService>();

        mapper.MapEndpoints<Item>(app, "storage");
        mapper.MapEndpoints<Location>(app, "location");
    }
}