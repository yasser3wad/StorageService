using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Storage.CoreInfrastructure.CoreFeatures;
using Storage.Application.Models;
using Storage.Domain.Entities;

namespace Storage.Application.Features;
public class LocalStorageService : IStorageService
{
    public readonly FileStorageSettings _storageSettings;

    public LocalStorageService(IOptions<FileStorageSettings> storageSettings)
    {
        _storageSettings = storageSettings.Value;
        if (!Directory.Exists(_storageSettings.LocalStoragePath))
        {
            Directory.CreateDirectory(_storageSettings.LocalStoragePath);
        }
    }

    public async Task<string> UploadFileAsync(IFormFile file, string newFileName)
    {
        var filePath = Path.Combine(_storageSettings.LocalStoragePath, newFileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filePath;
    }
    public async Task<byte[]> DownloadFileAsync(string fileName)
    {
        var filePath = Path.Combine(_storageSettings.LocalStoragePath, fileName);
        if (File.Exists(filePath))
        {
            return await File.ReadAllBytesAsync(filePath);
        }
        else
            return null; // not exist
    }

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var filePath = Path.Combine(_storageSettings.LocalStoragePath, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return true;
    }
}
