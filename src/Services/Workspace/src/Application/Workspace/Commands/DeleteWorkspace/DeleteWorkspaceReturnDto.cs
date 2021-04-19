using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceReturnDto:IMapForm<Core.Entities.Workspace>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Core.Entities.Project> Projects { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Workspace, DeleteWorkspaceReturnDto>()
               ;
        }
    }
}
