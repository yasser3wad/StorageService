using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Models
{
    public class FileStorageSettings
    {
        public string DefaultStorage { get; set; }
        public string LocalStoragePath { get; set; }
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string APPBaseURL { get; set; }


    }
    public enum StorageType
    {
        Local,
        AzureBlob,
        AWS

    }
}
