using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces.Dto
{
    public class CreateWorkspaceDto : WorkspaceDto
    {

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<Workspace, CreateWorkspaceDto>();
        }
    }
}
