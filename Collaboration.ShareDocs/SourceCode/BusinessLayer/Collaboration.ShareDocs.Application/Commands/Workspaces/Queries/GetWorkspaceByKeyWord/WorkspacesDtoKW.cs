using AutoMapper;
using System;
using Collaboration.ShareDocs.Application.Common.Mapping;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspaceByKeyWord
{
    public class WorkspacesDtoKW : IMapForm<Persistence.Entities.Workspace>
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool BookMark { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Persistence.Entities.Workspace, WorkspacesDtoKW>();
        }

    }
}
