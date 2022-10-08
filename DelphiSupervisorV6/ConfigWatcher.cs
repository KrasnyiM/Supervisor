using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DelphiSupervisorV6
{
    public class ConfigWatcher : IConfigWatcher
    {
        private FileSystemWatcher watcher;

        private XmlSerializer xmlSerializer;
        
        public event ServicesHandler ServiceAdded;
        public event ServicesHandler ServiceRemoved;

        public ConfigWatcher()
        {
            watcher = new FileSystemWatcher()
            {
                Path = "D:\\DelphiSupervisorV6\\Services",
                Filter = "*.xml",
                EnableRaisingEvents = true,
            };
            xmlSerializer = new XmlSerializer(typeof(ConfiguredService));
        }
                
        public void Watch()
        {
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {            
            using (FileStream fs = new FileStream(e.FullPath, FileMode.Open))
            {
                ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(fs);
                ServiceAdded?.Invoke(service);
            }
        }

        public void OnDeleted(object source, FileSystemEventArgs e)
        {
            using (FileStream fs = new FileStream(e.FullPath, FileMode.Open))
            {
                ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(fs);
                ServiceRemoved?.Invoke(service);
            }
        }
    }
}
