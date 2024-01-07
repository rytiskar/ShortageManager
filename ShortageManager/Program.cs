using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortageManager.ClassLibrary.Repositories;
using ShortageManager.ClassLibrary.Services;

namespace ShortageManager;

class Program 
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = ActivatorUtilities.CreateInstance<App>(host.Services);
        app.Run(args);
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IShortageRepository, ShortageRepository>();
                services.AddSingleton<IShortageService, ShortageService>();
                services.AddSingleton<App>();
            });
    }
}
