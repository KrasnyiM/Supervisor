using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public interface IProcessProvider
    {
        public List<ProcessInfo> GetAllProcesses();

        public ProcessInfo GetByID(int processId);

        public ProcessInfo StartProcces(string path);

        public ProcessInfo KillProcces(int processId);

        public List<ProcessInfo> GetProcessByName(string processName);

    }
}
