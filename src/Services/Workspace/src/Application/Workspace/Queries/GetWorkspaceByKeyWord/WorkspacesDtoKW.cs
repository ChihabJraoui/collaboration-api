using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetWorkspaceByKeyWord
{
    public class WorkspacesDtoKW : IMapForm<Core.Entities.Workspace>
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool BookMark { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Workspace, WorkspacesDtoKW>();
        }

    }
}
