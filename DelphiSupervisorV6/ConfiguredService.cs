using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DelphiSupervisorV6
{
    public class ConfiguredService 
    {
        public string ServiceName { get; set; }       
        public string ApplicationPath { get; set; }
        public string FileName { get; set; }

        [XmlIgnore]
        public bool IsStarted { get; set; }
    }
}
