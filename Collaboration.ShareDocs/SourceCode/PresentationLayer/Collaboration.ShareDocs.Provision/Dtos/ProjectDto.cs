
using System;
using System.Collections.Generic;

namespace Collaboration.ShareDocs.Provision.Dtos
{
    public class ProjectDto : AuditableEntityDto
    {
        public ProjectDto()
        { 
        }
        public Guid ProjectId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public virtual IEnumerable<FolderDto> Folders { get; set; }

        public virtual WorkspaceDto Workspace { get; set; }
    }
}

 
