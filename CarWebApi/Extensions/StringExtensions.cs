using System.Runtime.InteropServices;

namespace CarWebApi.Extensions;

public static class StringExtensions
{
    public const string ConfigVersion = "2025.07.15";
    
    private const string NLogСonfig = "NLog.config";
    private static string AppSettingsFile => "appsettings.json";
    // {
    //     get
    //     {
    //         if (!string.IsNullOrEmpty(Env) && Env.Contains("docker"))
    //         {
    //             return "appsettings.docker.json";
    //         }
    //         
    //         return IsLinusOs ? "appsettings.linux.json" : "appsettings.windows.json";
    //     }
    // }
    
    public static readonly bool IsLinusOs = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    public static readonly string BaseDirectory = AppContext.BaseDirectory;
    public static string NLogСonfigBaseDirectoryPath = Path.Combine(BaseDirectory, NLogСonfig);
    public const string LinuxSettingsPath = "/etc/CarWebApi";
    public static string AppSettingsBaseDirectoryPath = Path.Combine(BaseDirectory, AppSettingsFile);
    
    public static readonly string CommonApplicationDataPath =
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
    
    public static readonly string Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    
    public static readonly string AppSettingsCommonAppPath =
        IsLinusOs
            ? Path.Combine(LinuxSettingsPath, AppSettingsFile)
            : Path.Combine(CommonApplicationDataPath, "ScadaServer", "Cfg", AppSettingsFile);
    
    public static readonly string NlogSettingsPath =
        IsLinusOs
            ? Path.Combine(LinuxSettingsPath, NLogСonfig)
            : Path.Combine(CommonApplicationDataPath, "CarWebApi", "Cfg", NLogСonfig);
}