using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace SystemRepairCar.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadBlobAsync(string containerName, string blobName, Stream fileStream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(fileStream, true);

            return blobClient.Uri.ToString();
        }

        public async Task<string[]> UploadBlobsAsync((string containerName, string blobName, Stream fileStream)[] blobs)
        {
            var tasks = blobs.Select(blob => UploadBlobAsync(blob.containerName, blob.blobName, blob.fileStream)).ToArray();
            return await Task.WhenAll(tasks);
        }
    }
}
