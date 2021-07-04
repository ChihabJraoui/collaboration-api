using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;

namespace Collaboration.ShareDocs.Application.Commands.Follows.Dto
{
    public class FollowerDto : IMapForm<Follow>
    {
        public Guid FollowerId { get; set; }

        public ApplicationUser Follower { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Follow, FollowerDto>();
        }
    }
}
