using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorLogic
{
    public enum CommandType
    {
        Start,
        Kill
    }
    public enum WatchCommand
    {
        Start,
        Stop
    }
    public record Command(string Path, int ProcessId, CommandType CommandType);
}
