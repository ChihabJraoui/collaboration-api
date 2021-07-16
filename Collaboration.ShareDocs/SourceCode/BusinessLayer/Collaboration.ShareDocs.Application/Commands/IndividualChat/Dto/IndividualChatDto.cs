using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto
{
    public class IndividualChatDto : IMapForm<Persistence.Entities.IndividualChat>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public ResponseUserDto From { get; set; }
        public Guid UserId { get; set; }
        public ResponseUserDto To { get; set; }
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Persistence.Entities.IndividualChat, IndividualChatDto>();
        }

    }
}
