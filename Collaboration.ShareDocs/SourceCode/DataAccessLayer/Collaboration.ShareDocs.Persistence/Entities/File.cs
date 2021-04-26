using Collaboration.ShareDocs.Persistence.Common;
using System;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class File : Component
    {
        public Guid FileId { get; set; }

        public byte[] Content { get; set; }

        //[Display(Name = "Size (bytes)")]
        //[DisplayFormat(DataFormatString = "{0:N0}")]
        //public long Size { get; set; }

        //[Display(Name = "Uploaded (UTC)")]
        //[DisplayFormat(DataFormatString = "{0:G}")]
        //public DateTime UploadDT { get; set; }
        public File(string name) : base(name)
        {

        }
        public string FilePath { get; set; }
    }
}
