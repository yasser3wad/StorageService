using Microsoft.AspNetCore.Http;
using Storage.CoreInfrastructure.CoreDAL;
using Storage.CoreInfrastructure.CoreFeatures;
using Storage.Application.Models;
using Storage.Domain.Entities;
using Microsoft.Extensions.Options;
using Storage.CoreInfrastructure.DTO;

namespace Storage.Application.Features
{
    public class FileManagerService : IFileManagerService
    {
        public readonly FileStorageSettings _storageSettings;
        public readonly IStorageService _storageService;
        public readonly IUnitOfWork _unitofwork;

        public FileManagerService(IOptions<FileStorageSettings> storageSettings, IUnitOfWork unitofwork, IStorageService storageService)
        {
            _storageSettings = storageSettings.Value;
            _unitofwork = unitofwork;
            _storageService = storageService;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            // the system  generate new unique name for file if needed  
            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            await _storageService.UploadFileAsync(file, newFileName);
            var fileMetadata = new FileMetadata
            {
                FileName = newFileName,
                Size = file.Length,
                UploadedOn = DateTime.UtcNow
            };
            await _unitofwork.FileRepo.InsertAsync(fileMetadata);
            var saved = await _unitofwork.SaveChangesAsync();
            // return file name to be used in get file itself later
            return saved > 0 ? fileMetadata.FileName : string.Empty;
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            var fileMetadata = _unitofwork.FileRepo.Query(f => f.FileName == fileName).FirstOrDefault();
            if (fileMetadata == null)
            {
                return null;
            }
            var file = await _storageService.DownloadFileAsync(fileName);
            return file;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var fileMetadata = _unitofwork.FileRepo.Query(f => f.FileName == fileName).FirstOrDefault();

            if (fileMetadata != null)
            {
                await _storageService.DeleteFileAsync(fileMetadata.FileName);
                _unitofwork.FileRepo.Delete(fileMetadata);
                var deleted = await _unitofwork.SaveChangesAsync();
                return deleted > 0;
            }
            return false;
        }
        public async Task<FileMetadataResponse> RetrieveFileMetaDataAsync(string fileName)
        {
            var fileMetadata = _unitofwork.FileRepo.Query(f => f.FileName == fileName).FirstOrDefault();
            if (fileMetadata == null)
            {
                return null;
            }
            return new FileMetadataResponse(fileMetadata);

        }

    }
}
