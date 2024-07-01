using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Domain.Entities;

namespace Storage.Persistence.Config
{
    public class FileMetadataConfig : IEntityTypeConfiguration<FileMetadata>
    {
        public void Configure(EntityTypeBuilder<FileMetadata> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FileName).IsRequired();

        }
    }
}
