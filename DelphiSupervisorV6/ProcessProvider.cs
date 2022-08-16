using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace DelphiSupervisorV6
{
    public class ProcessProvider : IProcessProvider
    {
        private IServicesTable _servicesTable;

        public event ConfiguredServiceHandle ConfiguredServiceStarted;
        public event ConfiguredServiceHandle ConfiguredServiceStopped;
        private Task _monitorTask;
        public ProcessProvider(IServicesTable servicesTable)
        {
            _servicesTable = servicesTable;
        }

        event ConfiguredServiceHandle IProcessProvider.ConfiguredServiceStarted
        {
            add
            {
                ConfiguredServiceStarted += value;
            }

            remove
            {
                ConfiguredServiceStarted -= value;
            }
        }

        event ConfiguredServiceHandle IProcessProvider.ConfiguredServiceStopped
        {
            add
            {
                ConfiguredServiceStopped += value;
            }

            remove
            {
                ConfiguredServiceStopped -= value;
            }
        }

        public List<ProcessInfo> GetAllProcesses()
        {
            List<ProcessInfo> list = new List<ProcessInfo>();
            foreach(Process process in Process.GetProcesses())
            {
                list.Add(new ProcessInfo(process.ProcessName,process.Id, process.PagedMemorySize64));
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
        public void StartMonitorConfiguredServices()
        {
            _monitorTask = new Task(StartTimer);
            _monitorTask.Start();
        }

        private void StartTimer()
        {
            TimerCallback tm = new TimerCallback(MonitoreConfigureServices);
            Timer timer = new Timer(tm,null,0,2000);
        }

        private void MonitoreConfigureServices(object obj)
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
