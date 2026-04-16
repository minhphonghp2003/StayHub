using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Model.S3
{
    public class MinioCloudModel
    {
        public string FolderName { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string PublicUrl { get; set; } = string.Empty;
    }
}
