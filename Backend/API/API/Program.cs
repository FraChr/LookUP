using API.Extensions;
using API.Extensions.CorsExtensions;
using API.Extensions.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCustomCors();
builder.Services.AddAppServices();


var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
app.UseCustomCors();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.MapStorageEndpoints();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
