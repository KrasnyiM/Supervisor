using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    internal class ConsoleView : IView
    {
        public void ShowAll(IEnumerable<ProcessInfo> processes)
        {
            foreach (ProcessInfo process in processes)
            {
                Console.WriteLine($"ID: {process.PID}  Name: {process.Name} MemoryUsage: {process.Memory}");
            }
        }

        public void ShowMonitoredServiceStarted(ConfiguredService service)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Service {service.ServiceName} is started");
        }

        public void ShowMonitoredServiceStopped(ConfiguredService service)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Service {service.ServiceName} is stopped");
        }

        public void ShowNewConfig(ConfiguredService config)
        {
            Console.WriteLine($"Service {config.ServiceName} was configured for tracking");
        }

        public void ShowOne(ProcessInfo process)
        {
            Console.WriteLine($"ID: {process.PID}  Name: {process.Name} MemoryUsage: {process.Memory}");
        }
    }
}
