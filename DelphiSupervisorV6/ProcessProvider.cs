using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace DelphiSupervisorV6
{
    public class ProcessProvider : IProcessProvider
    {
        private IServicesTable _servicesTable;
        private static System.Timers.Timer _timer;

        public event ConfiguredServiceHandle ConfiguredServiceStarted;
        public event ConfiguredServiceHandle ConfiguredServiceStopped;

        public ProcessProvider(IServicesTable servicesTable)
        {
            _servicesTable = servicesTable;
        }       

        public List<ProcessInfo> GetAllProcesses()
        {
            List<ProcessInfo> list = new List<ProcessInfo>();
            foreach(Process process in Process.GetProcesses())
            {
                list.Add(new ProcessInfo(process.ProcessName, process.Id, process.PagedMemorySize64));
            }
            return list;
        }

        public ProcessInfo GetByID(int processId)
        {
            var process = Process.GetProcessById(processId);
            ProcessInfo processInfo = new ProcessInfo(process.ProcessName, process.Id, process.PagedMemorySize64);
            return processInfo;
        }

        public List<ProcessInfo> GetProcessByName(string processesName)
        {
            List<ProcessInfo> list = new List<ProcessInfo>();
            foreach(Process process in Process.GetProcessesByName(processesName))
            {
                list.Add(new ProcessInfo(process.ProcessName, process.Id, process.PagedMemorySize64));
            }
            return list;
        }

        public ProcessInfo KillProcces(int processesId)
        {
            var processById = Process.GetProcessById(processesId);
            ProcessInfo process = new ProcessInfo(processById.ProcessName, processById.Id, processById.PagedMemorySize64);
            processById.Kill();
            return process;
        }

        public ProcessInfo StartProcces(string path)
        {
            var process = Process.Start(path);
            ProcessInfo processInfo = new ProcessInfo(process.ProcessName, process.Id, process.PagedMemorySize64);
            return processInfo;
        }

        public void StartTimer()
        {
            _timer = new System.Timers.Timer(250);
            _timer.Elapsed += MonitoreConfigureServices;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public List<ProcessInfo> GetAllConfigureServices()
        {
            List<ProcessInfo> list = new List<ProcessInfo>();
            var runningProcesses = Process.GetProcesses();
            var cfgProcess = _servicesTable.GetServices();

            foreach(var service in runningProcesses)
            {
                if(cfgProcess.Select(p => p.ServiceName).Contains(service.ProcessName.Split('.')[0]))
                {                    
                    list.Add(new ProcessInfo(service.ProcessName,service.Id, service.PagedMemorySize64));                    
                }
            }
            return list;
        }

        private void MonitoreConfigureServices(object obj, ElapsedEventArgs e)
        {
            var runningProcesses = Process.GetProcesses();
            foreach (var service in _servicesTable.GetServices())
            {
                if (runningProcesses.Select(p => p.ProcessName).Contains(service.ServiceName) && !service.IsStarted)
                {
                    service.IsStarted = true;
                    ConfiguredServiceStarted?.Invoke(service);
                }
                if (service.IsStarted && !runningProcesses.Select(p => p.ProcessName).Contains(service.ServiceName))
                {
                    service.IsStarted = false;
                    ConfiguredServiceStopped?.Invoke(service);
                }
            }
        }
    }
}
