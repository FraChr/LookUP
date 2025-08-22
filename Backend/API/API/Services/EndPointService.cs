using API.Services.Interfaces;

namespace API.Services;

public class EndpointMapperService : IEndpointMapper
{
    public void MapEndpoints<T>(WebApplication app, string route)
    {
        app.MapGet($"/{route}", (int? limit, int? page, ICrudService<T> storage) =>
        {
            Console.WriteLine($"Limit: {limit}, Page: {page}");

            return storage.GetAll(limit, page);
        });

        app.MapPost($"/{route}", (T item, ICrudService<T> storage) =>
        {
            try
            {
                storage.Create(item);
                return Results.Ok(storage);
            }
            catch (Exception e)
            {
                return Results.BadRequest(new {
                    message = e.Message
                });
            }
        });
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