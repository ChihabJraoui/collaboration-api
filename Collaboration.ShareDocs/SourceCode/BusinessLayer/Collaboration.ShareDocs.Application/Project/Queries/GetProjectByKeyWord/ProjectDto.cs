using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Queries.GetProjectByKeyWord
{
    public class ProjectDto : IMapForm<Core.Entities.Project>
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public virtual Core.Entities.Workspace Workspace { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Project, ProjectDto>();
        }

    }
}
