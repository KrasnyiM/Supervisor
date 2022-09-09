using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public class ConfiguredServicesTable: IServicesTable
    {
        private List<ConfiguredService> configuredServices = new List<ConfiguredService>();

        private ConfigWatcher watcher;
        private IView view;

        public ConfiguredServicesTable(ConfigWatcher fileWatcher, IView view)
        {
            watcher = fileWatcher;
            watcher.ServiceAdded += AddService;
            watcher.ServiceRemoved += Delete;
            this.view = view;
        }

        public List<ConfiguredService> GetServices()
        {
            return configuredServices;
        }

        private void AddService(ConfiguredService configuredService)
        {
            configuredServices.Add(configuredService);
            view.ShowNewConfig(configuredService);
        }

        private void Delete(ConfiguredService configuredService)
        {
            var serviceToDelete = configuredServices.Where(s => s.ServiceName == configuredService.ServiceName).FirstOrDefault();
            configuredServices.Remove(serviceToDelete);
        }

    }
}
