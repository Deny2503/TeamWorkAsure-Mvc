using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebApplication16.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _container;

        public BlobService(IConfiguration config)
        {
            var connectionString = config["AzureStorage:ConnectionString"];
            var containerName = config["AzureStorage:ContainerName"];
            _container = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<string> UploadAsync(IFormFile file, string fileName)
        {
            var blobClient = _container.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                });
            }

            return blobClient.Uri.ToString();
        }

        public async Task<Stream> DownloadAsync(string fileName)
        {
            var blobClient = _container.GetBlobClient(fileName);
            var download = await blobClient.DownloadAsync();
            return download.Value.Content;
        }
    }

}