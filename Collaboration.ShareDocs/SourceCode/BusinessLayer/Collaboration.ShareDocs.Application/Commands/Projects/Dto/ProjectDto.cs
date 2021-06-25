using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Projects.Dto
{
    public class ProjectDto : IMapForm<Project>
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>();
        }
    }
}
