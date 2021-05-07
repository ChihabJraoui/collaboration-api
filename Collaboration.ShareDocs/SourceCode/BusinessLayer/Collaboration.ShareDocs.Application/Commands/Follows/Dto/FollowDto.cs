using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Follows.Dto
{
    public class FollowDto : IMapForm<Follow>
    {
        public Guid FollowingId { get; set; }
       
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Follow, FollowDto>();
        }
    
    
    }
}
