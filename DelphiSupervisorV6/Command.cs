using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public enum CommandType
    {
        Start,
        Kill
    }

    public record Command(string Path, int ProcessId, CommandType CommandType);
}
