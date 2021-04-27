using System;
using System.Collections.Generic;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;

namespace Collaboration.ShareDocs.Application.Workspace.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceReturnDto:IMapForm<Collaboration.ShareDocs.Persistence.Entities.Workspace>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Persistence.Entities.Project> Projects { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Persistence.Entities.Workspace, DeleteWorkspaceReturnDto>()
               ;
        }
    }
}
