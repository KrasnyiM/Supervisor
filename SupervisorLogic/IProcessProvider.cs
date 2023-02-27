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
        /// <summary>
        /// Event which signals that the configured service is running.
        /// </summary>
        public event ConfiguredServiceHandle ConfiguredServiceStarted;
        /// <summary>
        /// Event which signals that the configured service is stopped.
        /// </summary>
        public event ConfiguredServiceHandle ConfiguredServiceStopped;
        /// <summary>
        /// Method for get all processes.
        /// </summary>
        /// <returns>All processes that are running</returns>
        public List<ProcessInfo> GetAllProcesses();
        /// <summary>
        /// Method for get process on ID
        /// </summary>
        /// <param name="processId"></param>
        /// <returns>One process with specific ID</returns>
        public ProcessInfo GetByID(int processId);
        /// <summary>
        /// Method for start process.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Information about started process</returns>
        public ProcessInfo StartProcces(string path);
        /// <summary>
        /// Method for killed process.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns>Information about killed process</returns>
        public ProcessInfo KillProcces(int processId);
        /// <summary>
        /// Method for get process on name.
        /// </summary>
        /// <param name="processName"></param>
        /// <returns> on name</returns>
        public List<ProcessInfo> GetProcessByName(string processName);
        /// <summary>
        /// Method which monitor configured processes.
        /// </summary>
        //public void StartMonitorConfiguredServices();
        public void StartTimer();
        /// <summary>
        /// Method which return running configured services.
        /// </summary>
        /// <returns></returns>
        public List<ProcessInfo> GetAllConfigureServices();

        public void StopTimer();
    }
}
