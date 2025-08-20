using API.Model;
using API.Services.Interfaces;

namespace API.Services.ItemServices;

public class EndpointMapperService : IEndpointMapper
{
    // public void Endpoints(WebApplication app)
    // {
    //     app.MapGet("/storage", (IItemStorage<Items> storage) =>
    //     {
    //         return storage.GetItems();
    //     });
    //
    //     app.MapGet("/location", (IItemStorage<Location> storage) =>
    //     {
    //         return storage.GetItems();
    //     });
    //
    //     // app.MapPost("/storage", () =>
    //     // {
    //     //
    //     // });
    //     //
    //     // app.MapPut("/storage/{id}", () =>
    //     // {
    //     //
    //     // });
    //
    //     app.MapDelete("/storage/{id}", (int id, IItemStorage<Items> storage) =>
    //     {
    //         storage.DeleteItem(id);
    //         return Results.NoContent();
    //     });
    // }

    // public void MapStorageEndpoints<T>(WebApplication app, string route)
    // {
    //     app.MapGet($"/{route}", (IItemStorage<T> storage) =>
    //     {
    //         return storage.GetItems();
    //     });
    // }


    public void MapEndpoints<T>(WebApplication app, string route)
    {
        app.MapGet($"/{route}", (ICrudService<T> storage) =>
        {
            return storage.GetAll();
        });

        app.MapDelete($"/{route}/{{id}}", (int id, ICrudService<T> storage) =>
        {
            storage.Delete(id);
            return Results.NoContent();
        });
    }
}