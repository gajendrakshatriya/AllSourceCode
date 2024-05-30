using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureFunWithServiceBus
{
    public class BlobTriggerFunction
    {

        //[FunctionName("BlobTriggerFunctionWithProp")]
        //public static void Run([BlobTrigger("%BlobContainerName%/{name}", Connection = "BlobConnectionString")] CloudBlockBlob myBlob, string name, ILogger log)
        //{
        //    string a = myBlob.Properties.ContentType;
        //    log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Properties.Length} Bytes" + "\n" + a);
        //}

        [FunctionName("BlobTriggerFunction")]
        public static async Task Run([BlobTrigger("%BlobContainerName%/{name}", Connection = "BlobConnectionString")]
        Stream blobStream,
        string name,
        //BlobProperties blobProperties,
        //CloudBlockBlob cloudBlockBlob,
        ILogger log)
        {
            try
            {
                // Access blob properties using Azure Storage SDK
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                //CloudBlockBlob blob = container.GetBlockBlobReference(name);
                CloudBlob blob = container.GetBlobReference(name);
                await blob.FetchAttributesAsync(); // Retrieve blob properties

                log.LogInformation($">>old Blob date: {blob.Properties.LastModified.GetValueOrDefault()}");
                //log.LogInformation($">>old Blob date: {cloudBlockBlob.Properties.LastModified.GetValueOrDefault()}");
                //log.LogInformation($">>old Blob date: {blobProperties.LastModified.GetValueOrDefault()}");
                ////if (blobProperties.LastModified < functionDeploymentTime)
                //if (cloudBlockBlob.Properties.LastModified < functionDeploymentTime)
                //{
                //    log.LogInformation($">>old Blob name: {name}");
                //    return;
                //}

                log.LogInformation($">>Blob name: {name}");
                //using (StreamReader reader = new StreamReader(blobStream))
                //{
                //    string content = reader.ReadToEnd();
                //    log.LogInformation($">>Blob name: {name}");
                //    //log.LogInformation($">>Blob content: {content}");
                //}
            }
            catch (Exception ex)
            {
                log.LogError($"Error reading blob: {ex.Message}");
            }
        }



        /* //Renovar Blob Trigger
        [FunctionName("RenovarBlobTriggerFunction")]
        public static void RenovarBlobTriggerFunctionRun([BlobTrigger("%RenovarBlobContainerName%/{name}", Connection = "RenovarBlobConnectionString")] Stream blobStream,
        string name,
        ILogger log)
        {
            try
            {
                ////if (blobProperties.LastModified < functionDeploymentTime)
                //if (cloudBlockBlob.Properties.LastModified < functionDeploymentTime)
                //{
                //    log.LogInformation($">>old Blob name: {name}");
                //    return;
                //}

                using (StreamReader reader = new StreamReader(blobStream))
                {
                    string content = reader.ReadToEnd();
                    log.LogInformation($">>Blob name: {name}");
                    //log.LogInformation($">>Blob content: {content}");
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error reading blob: {ex.Message}");
            }
        }
        */
    }
}
