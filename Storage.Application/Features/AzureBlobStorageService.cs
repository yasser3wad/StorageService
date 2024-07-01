using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Storage.CoreInfrastructure.CoreFeatures;
using Storage.Application.Models;
using Microsoft.Extensions.Options;

namespace Storage.Application.Features;
public class AzureBlobStorageService : IStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    public readonly FileStorageSettings _storageSettings;
    public AzureBlobStorageService(IOptions<FileStorageSettings> storageSettings)
    {
        _storageSettings = storageSettings.Value;
        _blobServiceClient = new BlobServiceClient(_storageSettings.ConnectionString);
    }

    public async Task<string> UploadFileAsync(IFormFile file, string newFileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_storageSettings.ContainerName);
        var blobClient = containerClient.GetBlobClient(newFileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return blobClient.Uri.ToString();
    }

    public async Task<byte[]> DownloadFileAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_storageSettings.ContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var downloadInfo = await blobClient.DownloadAsync();
        using (var memoryStream = new MemoryStream())
        {
            await downloadInfo.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_storageSettings.ContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var deleted = await blobClient.DeleteIfExistsAsync();
        return deleted;
    }

}
