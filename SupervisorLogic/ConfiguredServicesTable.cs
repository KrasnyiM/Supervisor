using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DelphiSupervisorV6
{
    public class ConfiguredServicesTable: IServicesTable
    {
        private List<ConfiguredService> configuredServices = new List<ConfiguredService>();

        private IConfigWatcher watcher;

        public ConfiguredServicesTable(IConfigWatcher fileWatcher)
        {
            watcher = fileWatcher;
            watcher.ServiceAdded += AddService;
            watcher.ServiceRemoved += Delete;
            watcher.ConfigureServiceAdded += AddConfigureService;
        }

        public List<ConfiguredService> GetServices()
        {
            return configuredServices;
        }

        private void AddConfigureService(ConfiguredService configureService)
        {
            configuredServices.Add(configureService);
        }

        private void AddService(ConfiguredService configuredService)
        {
            configuredServices.Add(configuredService);
        }

        private void Delete(string fileName)
        {
            var serviceToDelete = configuredServices.Where(s => s.ServiceName == fileName)
                .FirstOrDefault();
            configuredServices.Remove(serviceToDelete);
        }
    }
}
