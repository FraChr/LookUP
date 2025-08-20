using API.Services.Interfaces;

namespace API.Services;

public class EndpointMapperService : IEndpointMapper
{
    public void MapEndpoints<T>(WebApplication app, string route)
    {
        app.MapGet($"/{route}", (ICrudService<T> storage) =>
        {
            return storage.GetAll();
        });

        // app.MapPost($"/{route}", (ICrudService<T> storage) =>
        // {
        //
        // });
        //
        // app.MapPut($"/{route}/{{id}}", (int id, ICrudService<T> storage) =>
        // {
        //
        // });

        app.MapDelete($"/{route}/{{id}}", (int id, ICrudService<T> storage) =>
        {
            storage.Delete(id);
            return Results.NoContent();
        });
    }
}