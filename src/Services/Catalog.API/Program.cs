using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Catalog.API.Data;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);
//Add services in container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(opts => { opts.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
// Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });


app.Run();