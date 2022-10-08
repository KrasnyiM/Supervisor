using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public delegate void ServicesHandler(ConfiguredService service);
    public interface IConfigWatcher
    {
        public event ServicesHandler ServiceAdded;
        public event ServicesHandler ServiceRemoved;

        public void Watch();

        public void OnCreated(object source, FileSystemEventArgs e);

        public void OnDeleted(object source, FileSystemEventArgs e);
    }
}
