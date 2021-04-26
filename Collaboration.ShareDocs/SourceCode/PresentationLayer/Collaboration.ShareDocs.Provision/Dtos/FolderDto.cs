using Collaboration.ShareDocs.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Collaboration.ShareDocs.Provision.Dtos
{
    public class FolderDto : Component
    {
        public Guid FolderId { get; set; }
        public FolderDto(string name) : base(name)
        {
            Components = new List<FolderDto>(); 
        }
        public IEnumerable<FolderDto> Components { get; set; }
        public virtual ProjectDto Project { get; set; }
        public virtual IEnumerable<FileDto> Files { get; set; }


    }

}
