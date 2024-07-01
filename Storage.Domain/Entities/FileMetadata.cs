﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Domain.Entities
{
    public class FileMetadata
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime UploadedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
