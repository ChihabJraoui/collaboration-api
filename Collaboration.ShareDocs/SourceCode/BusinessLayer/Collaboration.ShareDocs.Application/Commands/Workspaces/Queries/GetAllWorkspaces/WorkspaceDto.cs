using AutoMapper;
using System;
using System.Collections.Generic;
using Collaboration.ShareDocs.Application.Common.Mapping;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetAllWorkspaces
{
    public class WorkspaceDto:IMapForm<Collaboration.ShareDocs.Persistence.Entities.Workspace>
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public bool BookMark { get; set; }
        public bool IsPrivate { get; set; }


        public virtual ICollection<Collaboration.ShareDocs.Persistence.Entities.Project> Projects { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Collaboration.ShareDocs.Persistence.Entities.Workspace, WorkspaceDto>();
        }
    }
}
