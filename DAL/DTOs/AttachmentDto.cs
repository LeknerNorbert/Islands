using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class AttachmentDto
    {
        public Stream? File { get; set; }
        public string FileType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
