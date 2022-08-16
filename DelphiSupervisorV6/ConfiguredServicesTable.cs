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

        private FileWatcher watcher;
        public ConfiguredServicesTable(FileWatcher fileWatcher)
        {
            watcher = fileWatcher;
            watcher.ServiceAdded += AddService;
            watcher.ServiceRemoved += Delete;
        }

        public List<ConfiguredService> GetServices()
        {
            return configuredServices;
        }

        private void AddService(ConfiguredService configuredService)
        {
            configuredServices.Add(configuredService);
        }

        private void Delete(ConfiguredService configuredService)
        {
            var serviceToDelete = configuredServices.Where(s => s.ServiceName == configuredService.ServiceName).FirstOrDefault();
            configuredServices.Remove(serviceToDelete);
        }

    }
}
