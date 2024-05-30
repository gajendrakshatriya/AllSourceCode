using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunWithServiceBus
{
    //public class Startup : FunctionsStartup
    //{
    //    public override void Configure(IFunctionsHostBuilder builder)
    //    {
    //        builder.Services.AddLogging();
            
    //        // Register the BlobServiceClient using connection string
    //        builder.Services.AddSingleton(serviceProvider =>
    //        {
    //            var connectionString = "your-storage-connection-string";
    //            return new BlobServiceClient(connectionString, new BlobClientOptions { });
    //        });
    //    }
    //}
}
