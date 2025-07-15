using CarWebApi;
using CarWebApi.Database;
using CarWebApi.Mappings;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

using static CarWebApi.Extensions.StringExtensions;

namespace CarWebApi;

public static class Program
{
    public static async Task Main(string[] args)
    {

        CopySettingFiles();
        var logger = GetLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Configuration.AddEnvironmentVariables();

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

            app.Use(async (context, next) =>
            {
                logger.Info($"Request: {context.Request.Method} {context.Request.Path}");
                await next();
                logger.Info($"Response: {context.Response.StatusCode}");
            });

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
                catch (Exception ex)
                {
                    logger.Fatal(ex, ex.Message);
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
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
    
    static void CopySettingFiles()
    {
        if (!File.Exists(AppSettingsCommonAppPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(AppSettingsCommonAppPath)!);
            File.Copy(AppSettingsBaseDirectoryPath, AppSettingsCommonAppPath);
        }
        else
        {
            var localConfig = new ConfigurationBuilder();
            localConfig.AddJsonFile("appsettings.json", true, true);
            localConfig.AddJsonFile(AppSettingsCommonAppPath, true, true);
            localConfig.AddEnvironmentVariables();
            var configurationRoot = localConfig.Build();
            var configurationVersion = configurationRoot.GetSection("ConfigVersion").Value;

            if (string.IsNullOrEmpty(configurationVersion) || !Equals(configurationVersion, ConfigVersion))
            {
                BackupFile(AppSettingsCommonAppPath);
                Directory.CreateDirectory(Path.GetDirectoryName(AppSettingsCommonAppPath)!);
                File.Copy(AppSettingsBaseDirectoryPath, AppSettingsCommonAppPath);
            }
        }

        if (!File.Exists(NlogSettingsPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(NlogSettingsPath)!);
            File.Copy(NLogСonfigBaseDirectoryPath, NlogSettingsPath);
        }
    }
    
    static Logger GetLogger()
    {
        var logger = File.Exists(NlogSettingsPath)
            ? LogManager.Setup().LoadConfigurationFromFile(NlogSettingsPath).GetCurrentClassLogger()
            : LogManager.Setup().RegisterNLogWeb().GetCurrentClassLogger();
        LogManager.Configuration.Variables["IsLinusOs"] = IsLinusOs.ToString();

        return logger;
    }
    
    static void BackupFile(string file)
    {
        string BackupFilePrefix = "bkp.";
        var fileName = Path.GetFileName(file);
        var directoryName = Path.GetDirectoryName(file);
        var count = 1;
        string backupFileName;
        do
        {
            backupFileName = Path.Combine(directoryName, $"{BackupFilePrefix}{fileName} ({count++})");
        } while (File.Exists(backupFileName));

        File.Move(file, backupFileName);
    }
}