using System;
using ConsoleAppFramework;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;


namespace DelphiSupervisorV6
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            var builder = ConsoleApp.CreateBuilder(args);
            builder.ConfigureServices((ctx, services) =>
            {
                services.AddSingleton<IConfigWatcher, ConfigWatcher>();
                services.AddSingleton<IView, ConsoleView>();
                services.AddSingleton<IProcessProvider, SdkProcessProvider>();
                services.AddSingleton<IServicesTable, ConfiguredServicesTable>();
            });           

            var app = builder.Build();
            app.Services.GetRequiredService<IConfigWatcher>().Watch();
            app.AddCommands<Commands>();
            app.Run();
            Console.ReadLine();
        }
    }  
}