using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.CoreInfrastructure.DTO;

public class FileMetadataResponse
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public long Size { get; set; }
    public DateTime UploadedOn { get; set; }
    public FileMetadataResponse(FileMetadata entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            FileName = entity.FileName;
            Size = entity.Size;
            UploadedOn = entity.UploadedOn;

        }
    }
}
