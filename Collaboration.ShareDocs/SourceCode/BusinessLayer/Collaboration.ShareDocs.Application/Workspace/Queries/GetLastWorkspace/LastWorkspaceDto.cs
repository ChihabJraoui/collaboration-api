using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetLastWorkspace
{
    public class LastWorkspaceDto:IMapForm<Core.Entities.Workspace>
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
            profile.CreateMap<Core.Entities.Workspace, LastWorkspaceDto>();
        }
    }
}
