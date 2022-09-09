using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiSupervisorV6
{
    public interface IServicesTable
    {
        /// <summary>
        /// Method for get configured services.
        /// </summary>
        /// <returns></returns>
        public List<ConfiguredService> GetServices();
    }
}
