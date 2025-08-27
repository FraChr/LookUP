using API.Services.Interfaces;

namespace API.Services;

public class EndpointMapperService : IEndpointMapper
{
    public void MapEndpoints<T>(WebApplication app, string route)
    {
        app.MapGet($"/{route}", (string? searchTerm, int? limit, int? page, ICrudService<T> storage) =>
        {
            Console.WriteLine($"Limit: {limit}, Page: {page}");

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return storage.Search(searchTerm, limit, page);
            }

            return storage.GetAll(limit, page);
        });

        app.MapGet($"/{route}/{{id}}", (int id, ICrudService<T> storage) =>
        {
            return storage.GetById(id);
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

        app.MapPut($"/{route}/{{id}}", (T item, int id, ICrudService<T> storage) =>
        {
            try
            {
                var updatedItem = storage.Update(item, id);
                return Results.Ok(updatedItem);

            }
            catch (Exception e)
            {
                return Results.Problem(e.Message, statusCode: 400);
            }
        });

        app.MapDelete($"/{route}/{{id}}", (int id, ICrudService<T> storage) =>
        {
            storage.Delete(id);
            return Results.NoContent();
        });
    }
}