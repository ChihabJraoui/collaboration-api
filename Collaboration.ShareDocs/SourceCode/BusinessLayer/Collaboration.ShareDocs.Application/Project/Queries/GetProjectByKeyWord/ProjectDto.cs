using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Queries.GetProjectByKeyWord
{
    public class ProjectDto : IMapForm<Collaboration.ShareDocs.Persistence.Entities.Project>
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public virtual Collaboration.ShareDocs.Persistence.Entities.Workspace Workspace { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Collaboration.ShareDocs.Persistence.Entities.Project, ProjectDto>();
        }

    }
}
