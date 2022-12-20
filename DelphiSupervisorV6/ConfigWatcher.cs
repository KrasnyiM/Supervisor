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

        public ConfigWatcher()
        {
            watcher = new FileSystemWatcher()
            {
                Path = "D:\\Service",
                Filter = "*.xml",
                EnableRaisingEvents = true,
            };
            xmlSerializer = new XmlSerializer(typeof(ConfiguredService));
        }

        public void Watch()
        {
            Init();
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
        }

        private void Init()
        {
            var files = Directory.GetFiles("D:\\Service", "*.xml");
            foreach (var file in files)
            {
                using (var reader = XmlReader.Create(file))
                {
                    ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(reader);
                    ServiceAdded?.Invoke(service);
                }
            }
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            using (XmlReader reader = XmlReader.Create(e.FullPath))
            {
                ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(reader);
                service.FileName = e.Name;
                ServiceAdded?.Invoke(service);
            }
        }

        public void OnDeleted(object source, FileSystemEventArgs e)
        {
            ServiceRemoved?.Invoke(e.Name);
        }
    }
}
