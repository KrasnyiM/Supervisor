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
                services.AddSingleton<IView, ConsoleView>();
                services.AddSingleton<IProcessProvider, ProcessProvider>();
            });
            

            var app = builder.Build();
            app.AddCommands<Commands>();
            app.Run();
            Console.ReadLine();
        }

    }
    
}