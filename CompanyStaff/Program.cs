using CompanyStaff.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));

//Console.WriteLine(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));
//LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));

builder.Services.ConfigCORS();
builder.Services.ConfigIISIntegration();
builder.Services.ConfigLoggerService();
builder.Services.ConfigRepositoryManager();
builder.Services.ConfigServiceManager();
builder.Services.ConfigSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CompanyStaff.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetRequiredService<ILoggerManager>();

app.ConfigExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicyAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
