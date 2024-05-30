using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureOperationsLib
{
    public class AzureConfigData
    {
        public string ServiceBusConnectionString { get; set; }
        public string ServiceBusPort { get; set; }
        public string ServiceBusQueue { get; set; }
        public string BlobConnectionString { get; set; }
        public string BlobContainerName { get; set; }
        public string NotificationHubName { get; set; }
        public string NotificationConnectionString { get; set; }
    }
}
