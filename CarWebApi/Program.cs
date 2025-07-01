using CarWebApi;
using CarWebApi.Database;
using CarWebApi.Mappings;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => { });
builder.Services.AddControllers();

var dbConfig = builder.Configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>();

builder.Services.AddDbContext<CarApiDbContext>(options =>
{
    var connectionStr = dbConfig?.PostgreSQLConnection;
    options.UseNpgsql(connectionStr);
});

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IgnoreObsoleteActions();
    c.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
});

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(CarApiDbContext).Assembly));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<CarApiDbContext>();
        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
            DbInitializer.Initialize(context);
        }
        
    }
    catch (Exception exception)
    {
        //Log.Fatal(exception, "");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();