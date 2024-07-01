using Microsoft.AspNetCore.Http;

namespace Storage.CoreInfrastructure.CoreFeatures;
public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file,string newFileName);
    Task<byte[]> DownloadFileAsync(string fileName);
    Task<bool> DeleteFileAsync(string fileName);
}
