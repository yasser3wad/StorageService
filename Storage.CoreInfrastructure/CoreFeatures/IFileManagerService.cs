using Microsoft.AspNetCore.Http;
using Storage.CoreInfrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.CoreInfrastructure.CoreFeatures
{
    public interface IFileManagerService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<byte[]> DownloadFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
        Task<FileMetadataResponse> RetrieveFileMetaDataAsync(string fileName);
    }
}
