using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public interface IServicesTable
    {
        public List<ConfiguredService> GetServices();

    }
}
