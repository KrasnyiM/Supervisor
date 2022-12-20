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
        private IView view;

        public ConfiguredServicesTable(IConfigWatcher fileWatcher, IView view)
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

        private void Delete(string fileName)
        {
            var serviceToDelete = configuredServices.Where(s => s.ServiceName == fileName)
                .FirstOrDefault();

            configuredServices.Remove(serviceToDelete);

            view.ShowDeleteConfig(fileName);
        }
    }
}
