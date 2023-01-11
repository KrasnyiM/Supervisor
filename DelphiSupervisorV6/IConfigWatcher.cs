using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public delegate void AddService(ConfiguredService service);
    public delegate void AddConfigureService(ConfiguredService service);
    public delegate void RemoveService(string fileName);
    public interface IConfigWatcher
    {
        /// <summary>
        /// Event which signals that the configured service is added.
        /// </summary>
        public event AddService ServiceAdded;
        /// <summary>
        /// Event which signals that the configured service is deleted.
        /// </summary>
        public event RemoveService ServiceRemoved;
        /// <summary>
        /// Event which signals that the configured service is added, but doesn`t display information about service.
        /// </summary>
        public event AddConfigureService ConfigureServiceAdded;
        /// <summary>
        /// Method which add configured services to list for command "Watch" at the first launch of the program.
        /// </summary>
        public void Init();
        /// <summary>
        /// Method which subscribes to events.
        /// </summary>
        public void Watch();
        /// <summary>
        /// Method which add configured services to list when add config. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnCreated(object source, FileSystemEventArgs e);
        /// <summary>
        /// Method which delete configure service from list when delete config.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnDeleted(object source, FileSystemEventArgs e);
        /// <summary>
        /// Method which add configured services to list for command "GetAll" at the first launch of the program.
        /// </summary>
        public void AddConfigureServices();
    }
}
