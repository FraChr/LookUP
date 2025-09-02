using API.Services.Interfaces;

namespace API;

public static class EndpointHandlers
{
    public static async Task<IResult> HandleGetAll<T>(
        string? searchTerm,
        int? limit,
        int? page,
        ICrudService<T> storage)
    {
        try
        {
            Console.WriteLine($"========================\nLimit: {limit}, Page: {page}");

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var result = await storage.Search(searchTerm, limit, page);
                return Results.Ok(result);
            }

            var all = await storage.GetAll(limit, page);
            return Results.Ok(all);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public static async Task<IResult> HandleGetById<T>(int id, ICrudService<T> storage)
    {
        var result = await storage.GetById(id);
        return Results.Ok(result);
    }

    public static async Task<IResult> HandleCreate<T>(T createdItem, ICrudService<T> storage)
    {
        try
        {
            await storage.Create(createdItem);
            return Results.Ok(storage);
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                message = e.Message,
            });
        }
    }

    public static async Task<IResult> HandleUpdate<T>(int id, T modifiedItem, ICrudService<T> storage)
    {
        try
        {
            var updatedItem = await storage.Update(modifiedItem, id);
            return Results.Ok(updatedItem);

        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: 400);
        }
    }

    public static async Task<IResult> HandleDelete<T>(int id, ICrudService<T> storage)
    {
        try
        {
            await storage.Delete(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: 500);
        }
    }

}