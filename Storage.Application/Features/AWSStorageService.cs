using Microsoft.AspNetCore.Http;
using Storage.CoreInfrastructure.CoreFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features
{
    public class AWSStorageService : IStorageService
    {
        public Task<string> UploadFileAsync(IFormFile file, string newFileName)
        {
            throw new NotImplementedException();
        }
        public Task<byte[]> DownloadFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }


    }
}
