using Carter;

var builder = WebApplication.CreateBuilder(args);
    //Add services in container
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });
    builder.Services.AddCarter();
var app = builder.Build();
// Configure the HTTP request pipeline
app.MapCarter();


app.Run();