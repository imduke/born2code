using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.Models
{
    public sealed class UploadedFileInfo
    {
        public string MimeType { get; set; }
        public Stream Stream { get; set; }
        public string FileName { get; set; }
    }
}
