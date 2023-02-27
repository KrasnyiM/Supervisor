using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DelphiSupervisorV6
{
    public class ConfigWatcher : IConfigWatcher
    {
        private FileSystemWatcher watcher;
        private XmlSerializer xmlSerializer;

        public event AddService ServiceAdded;
        public event RemoveService ServiceRemoved;
        public event AddConfigureService ConfigureServiceAdded; 

        public ConfigWatcher()
        {
            watcher = new FileSystemWatcher()
            {
                Path = "D:\\DelphiSupervisor_Test_Test\\Service",
                Filter = "*.xml",
                EnableRaisingEvents = true,
            };
            xmlSerializer = new XmlSerializer(typeof(ConfiguredService));
        }
        
        public void Watch()
        {
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
        }

        public void Init()
        {
            var files = Directory.GetFiles("D:\\DelphiSupervisor_Test_Test\\Service", "*.xml");
            foreach (var file in files)
            {
                using (var reader = XmlReader.Create(file))
                {
                    ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(reader);
                    OnServiceAdded(service);
                }
            }
        }

        public void AddConfigureServices()
        {
            var files = Directory.GetFiles("D:\\DelphiSupervisor_Test_Test\\Service", "*.xml");
            foreach (var file in files)
            {
                using (var reader = XmlReader.Create(file))
                {
                    ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(reader);
                    OnConfigureServiceAdded(service);
                }
            }
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            using (var reader = XmlReader.Create(e.FullPath))
            {
                ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(reader);
                service.FileName = e.Name;
                OnServiceAdded(service);
            }
        }

        public void OnDeleted(object source, FileSystemEventArgs e)
        {
            OnServiceRemoved(e.Name);
        }

        private void OnServiceAdded(ConfiguredService service)
        {
            ServiceAdded?.Invoke(service);
        }
        
        private void OnServiceRemoved(string service)
        {
            ServiceRemoved?.Invoke(service);
        }

        private void OnConfigureServiceAdded(ConfiguredService service)
        {
            ConfigureServiceAdded?.Invoke(service);
        }
        
    }
}
