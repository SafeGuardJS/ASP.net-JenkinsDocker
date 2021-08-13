using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiASP.Models
{
    
        public class MonitorstoreDatabaseSettings : IMonitorstoreDatabaseSettings
        {
            public string MonitorsCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IMonitorstoreDatabaseSettings
        {
            string MonitorsCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    
}
