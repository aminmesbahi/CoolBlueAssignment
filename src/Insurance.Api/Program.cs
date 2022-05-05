using Insurance.Api.Data;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5002");
                      });
});
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<InsuranceDbContext>(x => x.UseSqlite(connectionString));
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
    config.AddEnvironmentVariables();
    config.AddCommandLine(args);
});

builder.Services.AddControllers();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISurchargeService, SurchargeService>();
builder.Services.AddTransient<IInsuranceCalculationService, InsuranceCalculationService>();
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CoolBlue Insurance Calculator Service",
        Description = "Using these APIs you can get insurance calculation and add surcharge for product types."
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
await EnsureDbAsync(app.Services);

app.UseSwagger();
app.UseSwaggerUI(c => c.DefaultModelsExpandDepth(-1));


app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<InsuranceDbContext>();
    await db.Database.MigrateAsync();
}