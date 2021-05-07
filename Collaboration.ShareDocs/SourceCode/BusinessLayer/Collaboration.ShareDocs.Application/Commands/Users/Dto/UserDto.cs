using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Users.Dto
{
    public class UserDto : IMapForm<ApplicationUser>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserDto>();
        }
    
    }
}
