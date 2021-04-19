using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetLastModifiedWorkspace
{
    public class WorkspaceLastModifiedDto:IMapForm<Core.Entities.Workspace>
    {
        public string Name { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string Owner { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Workspace, WorkspaceLastModifiedDto>();
        }
    }
}
