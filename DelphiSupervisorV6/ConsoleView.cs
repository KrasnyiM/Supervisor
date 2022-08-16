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

        public void ShowOne(ProcessInfo process)
        {
            Console.WriteLine($"ID: {process.PID}  Name: {process.Name} MemoryUsage: {process.Memory}");
        }
    }
}
