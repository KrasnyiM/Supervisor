using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public delegate void AddService(ConfiguredService service);
    public delegate void RemoveService(string fileName);
    public interface IConfigWatcher
    {
        public event AddService ServiceAdded;
        public event RemoveService ServiceRemoved;

        public void Watch();

        public void OnCreated(object source, FileSystemEventArgs e);

        public void OnDeleted(object source, FileSystemEventArgs e);
    }
}
