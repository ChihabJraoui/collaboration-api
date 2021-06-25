using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Projects.Dto
{
    public class UsersProjectDto : IMapForm<Project>
    {
        public Guid UserId { get; set; }
       
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<UserProject, UsersProjectDto>();
        }
    }
}
