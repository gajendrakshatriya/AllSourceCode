using System;
using System.IO;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureFunWithServiceBus
{
    public class Function1
    {
        private static DateTime functionDeploymentTime = DateTime.UtcNow;
        [FunctionName("Function1")]
        public void Run([ServiceBusTrigger("sbus-queue-1", Connection = "SBConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($">>Run C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }

        [FunctionName("Function2")]
        public void Run2([ServiceBusTrigger("sbus-queue-1", Connection = "SBConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($">>Run 2 C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}


