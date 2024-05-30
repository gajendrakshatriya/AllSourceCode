using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using System.IO;

namespace AzureOperationsLib
{
    public interface IAzureBlobOP
    {
        Task UploadImageAsync(Stream imagePath, string imageName);
    }
    public class AzureBlobOP : IAzureBlobOP
    {
        private string _connectionString;
        private string _containerName;
        public AzureBlobOP(AzureConfigData azureConfigData)
        {
            _connectionString = azureConfigData.BlobConnectionString;
            _containerName = azureConfigData.BlobContainerName;
        }
        public async Task UploadImageAsync(Stream imagePath, string imageName)
        {
            // Create a BlobServiceClient object
            var blobServiceClient = new BlobServiceClient(_connectionString);

            // Get a reference to the container
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Create the container if it doesn't exist
            await blobContainerClient.CreateIfNotExistsAsync();

            // Get a reference to the blob
            var blobClient = blobContainerClient.GetBlobClient(imageName);

            await blobClient.UploadAsync(imagePath, true);
            // Upload the image file to the blob
            //using (var fileStream = File.OpenRead(imagePath))
            //{
            //    await blobClient.UploadAsync(fileStream, true);
            //}

            Console.WriteLine($">>Image '{imageName}' uploaded successfully.");
        }

    }
}