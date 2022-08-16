using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public delegate void ConfiguredServiceHandle(ConfiguredService service);
    public interface IProcessProvider
    {
        
        public event ConfiguredServiceHandle ConfiguredServiceStarted;
        public event ConfiguredServiceHandle ConfiguredServiceStopped;

        public List<ProcessInfo> GetAllProcesses();

        public ProcessInfo GetByID(int processId);

        public ProcessInfo StartProcces(string path);

        public ProcessInfo KillProcces(int processId);

        public List<ProcessInfo> GetProcessByName(string processName);
        public void StartMonitorConfiguredServices();

    }
}
