using System;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetLastWorkspace
{
    public class LastWorkspaceDto:IMapForm<Persistence.Entities.Workspace>
    {
        public string Name { get; set; }
        
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Owner { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Persistence.Entities.Workspace, LastWorkspaceDto>();
        }
    }
}
