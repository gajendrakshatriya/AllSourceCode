using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureOperationsLib.NotificationBO
{
    public class DeviceRegistration
    {
        public string Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }

    public static class PNSDeviceType
    {
        public const string Android = "fcm";
        public const string Apple = "apns";
        public const string Windows = "wns";
    }
}
