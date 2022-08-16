using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DelphiSupervisorV6
{
    public class ProcessProvider : IProcessProvider
    {
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
    }
}
