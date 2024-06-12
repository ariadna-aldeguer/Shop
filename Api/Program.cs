using Api;
using Api.Middlewares;
using AutoMapper;
using Data.Database;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);

// AutoMapper
var config = new MapperConfiguration(cfg => {
    cfg.AddMaps(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Map("/", () => Results.Redirect("/api"));

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
