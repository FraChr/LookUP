using API;
using API.Extensions;
using API.Extensions.CorsExtensions;
using API.Extensions.ServiceExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// var connectionBuilder = new ConnectionBuilder(configuration);

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(connectionBuilder.GetConnectionString()));

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionBuilder = serviceProvider.GetRequiredService<ConnectionBuilder>();
    options.UseSqlServer(connectionBuilder.GetConnectionString());
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCustomCors();
builder.Services.AddAppServices();


var app = builder.Build();


app.UseHttpsRedirection();
app.UseCustomCors();
app.UseRouting();


//Configure the HTTP request pipeline.
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
