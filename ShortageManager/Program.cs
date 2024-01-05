using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ShortageManager;

class Program 
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = ActivatorUtilities.CreateInstance<App>(host.Services);
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args);
    }
}
