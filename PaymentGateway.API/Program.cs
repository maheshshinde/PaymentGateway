using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentGateway.API.Middleware.ExceptionHandler;
using PaymentGateway.Application;
using PaymentGateway.Domain.Repository.Query;
using PaymentGateway.Infrastructure;
using PaymentGateway.Persistance;
using PaymentGateway.Persistance.Repository.Query;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "The API key to access the API End Points",
        Type = SecuritySchemeType.ApiKey,
        Name = "Api_Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header,
    };

    var requirements = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>()}
    };

    o.AddSecurityRequirement(requirements);
});

builder.Services.AddDbContext<PaymentDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("PaymentDbConnection"));
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistanceServices();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IPaymentQueryRepository, PaymentQueryRepository>();


var loggerSeriLog = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerSeriLog);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Create database & Tables
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var paymentsDdContext = services.GetRequiredService<PaymentDbContext>();

    await paymentsDdContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

app.Run();
