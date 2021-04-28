using System;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetLastModifiedWorkspace
{
    public class WorkspaceLastModifiedDto:IMapForm<Persistence.Entities.Workspace>
    {
        public string Name { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string Owner { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Persistence.Entities.Workspace, WorkspaceLastModifiedDto>();
        }
    }
}
