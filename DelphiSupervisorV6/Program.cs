using System;
using ConsoleAppFramework;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace DelphiSupervisorV6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = ConsoleApp.Create(args);
            app.AddCommands<Commands>();
            app.Run();
            Console.ReadLine();
        }

    }
    
}