using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DelphiSupervisorV6
{
    public  class ProcessInfo
    {
        public string Name { get; set; }
        public int PID { get; set; }
        public long Memory { get; set; }

        public ProcessInfo(Process process)
        {
            Name = process.ProcessName;
            PID = process.Id;
            Memory = process.PagedMemorySize64;
        }
    }
}
