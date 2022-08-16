using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DelphiSupervisorV6
{
    public class FileWatcher
    {
        private FileSystemWatcher watcher;

        private XmlSerializer xmlSerializer;

        public delegate void ServicesHandler(ConfiguredService service);
        public event ServicesHandler ServiceAdded;
        public event ServicesHandler ServiceRemoved;

        public FileWatcher()
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

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            using (FileStream fs = new FileStream(e.FullPath,FileMode.Open))
            {
                ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(fs);
                ServiceAdded?.Invoke(service);
            }
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            using (FileStream fs = new FileStream(e.FullPath, FileMode.Open))
            {
                ConfiguredService service = (ConfiguredService)xmlSerializer.Deserialize(fs);
                ServiceRemoved?.Invoke(service);
            }
        }
    }
}
