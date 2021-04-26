using System;

namespace Collaboration.ShareDocs.Provision.Dtos
{
    public class FileDto : ComponentDto
    {
        public Guid FileId { get; set; }

        public byte[] Content { get; set; }

        //[Display(Name = "Size (bytes)")]
        //[DisplayFormat(DataFormatString = "{0:N0}")]
        //public long Size { get; set; }

        //[Display(Name = "Uploaded (UTC)")]
        //[DisplayFormat(DataFormatString = "{0:G}")]
        //public DateTime UploadDT { get; set; }
        public FileDto(string name) : base(name)
        {

        }
        public string FilePath { get; set; }
    }
}
