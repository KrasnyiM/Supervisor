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
        /// <summary>
        /// Method for showing all processes
        /// </summary>
        /// <param name="processes">Processes to show</param>
        public void ShowAll(IEnumerable<ProcessInfo> processes);
        /// <summary>
        /// Method for showing one process
        /// </summary>
        /// <param name="process">Process to show</param>
        public void ShowOne(ProcessInfo process);
        /// <summary>
        /// Method for showing message that configured process stared.
        /// </summary>
        /// <param name="service">Strated service</param>
        public void ShowMonitoredServiceStarted(ConfiguredService service);
        /// <summary>
        /// Method for showing message that configured process stopped.
        /// </summary>
        /// <param name="service">Stopped service</param>
        public void ShowMonitoredServiceStopped(ConfiguredService service);
        /// <summary>
        /// Method for showing message that new config added.
        /// </summary>
        /// <param name="config">Added config</param>
        public void ShowNewConfig(ConfiguredService config);
    }
}
