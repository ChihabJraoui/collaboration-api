using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;

namespace Collaboration.ShareDocs.Application.Commands.Follows.Dto
{
    public class FollowerDto : IMapForm<ApplicationUser>
    {
        public Guid FollowerId { get; set; }

        public ResponseUserDto Follower { get; set; }
        

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, FollowerDto>();
        }
    }
}
