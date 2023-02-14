using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Formatting;

namespace DelphiSupervisorV6
{
    public class Commands : ConsoleAppBase
    {
        private IView view;
        private IConfigWatcher configWatcher;
        private ProcessProvider processProvider;

        public Commands(IView view, IConfigWatcher configWatcher)
        {
            this.view = view;
            this.configWatcher = configWatcher;
            processProvider = new ProcessProvider();
        }

        [Command("GetAll","Method that shows all running configured services")]
        public void GetAll()
        {
            view.ShowAll(processProvider.GetAllConfigureServices());
        }

        [Command("ShowAll","Method that shows all running processes on the system")]
        public void ShowAll()
        {
            view.ShowAll(processProvider.GetAllProcesses());
        }

        [Command("ShowById","Method that shows all running processes by id")]
        public void ShowById([Option(0)]int processId)
        {
            view.ShowOne(processProvider.GetByID(processId));
        }

        [Command("StartProcess","Method that starts a process")]
        public void StartProcess([Option(0)]string path)
        {            
            view.ShowOne(processProvider.StartProcces(path));
        }

        [Command("KillProcess","Method that kills a process")]
        public void KillProcess([Option(0)] int processId)
        {
            view.ShowOne(processProvider.KillProcces(processId));
        }

        [Command("ProcessByName", "Method that shows all running processes by name")]
        public void ProcessByName([Option(0)] string processName)
        {
            view.ShowAll(processProvider.GetProcessByName(processName));          
        }

        [Command("Watch", "Method that monitors configured services")]
        public void Watch()
        {
            configWatcher.ServiceAdded += view.ShowNewConfig;
            configWatcher.ServiceRemoved += view.ShowDeleteConfig;
            configWatcher.Init();
            processProvider.ConfiguredServiceStarted += Provider_ConfiguredServiceStarted;
            processProvider.ConfiguredServiceStopped += Provider_ConfiguredServiceStopped;
            processProvider.StartTimer();           
        }

        private void Provider_ConfiguredServiceStopped(ConfiguredService service)
        {
            view.ShowMonitoredServiceStopped(service);
        }

        private void Provider_ConfiguredServiceStarted(ConfiguredService service)
        {
            view.ShowMonitoredServiceStarted(service);
        }

    }
}