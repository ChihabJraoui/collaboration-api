using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat.Dto
{
    public class CreateGroupDto : IMapForm<Group>
    {
        public Guid GroupID { get; set; }
        public string Name { get; set; }
        public Guid Owner { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Group, CreateGroupDto>();
        }
    }
}
