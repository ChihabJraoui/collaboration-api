using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces.Dto
{
    public class WorkspaceDto : IMapForm<Workspace>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public bool BookMark { get; set; }
        public bool IsPrivate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workspace, WorkspaceDto>();
        }
    }
}
