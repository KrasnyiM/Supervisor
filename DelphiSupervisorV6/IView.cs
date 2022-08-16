using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DelphiSupervisorV6
{
    public interface IView
    {
        public void ShowAll(IEnumerable<ProcessInfo> processes);

        public void ShowOne(ProcessInfo process);
    }
}
