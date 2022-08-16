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
                list.Add(new ProcessInfo(process));
            }
            return list;
        }

        public ProcessInfo GetByID(int processId)
        {
            ProcessInfo process = new ProcessInfo(Process.GetProcessById(processId));
            return process;
        }

        public List<ProcessInfo> GetProcessByName(string processesName)
        {
            List<ProcessInfo> list = new List<ProcessInfo>();
            foreach(Process process in Process.GetProcessesByName(processesName))
            {
                list.Add(new ProcessInfo(process));
            }
            return list;
        }

        public ProcessInfo KillProcces(int processesId)
        {
            var processById = Process.GetProcessById(processesId);
            ProcessInfo process = new ProcessInfo(processById);
            processById.Kill();
            return process;
        }

        public ProcessInfo StartProcces(string path)
        {
            var process = Process.Start(path);
            ProcessInfo processInfo = new ProcessInfo(process);
            return processInfo;
        }
    }
}
