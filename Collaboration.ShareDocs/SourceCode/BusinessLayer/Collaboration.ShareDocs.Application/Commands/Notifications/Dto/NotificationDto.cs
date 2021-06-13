using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;

namespace Collaboration.ShareDocs.Application.Commands.Notifications.Dto
{
    public class NotificationDto : IMapForm<Notification>
    {
        public Guid NotificationID { get; set; }
        //public string Text { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<NotificationApplicationUser, NotificationDto>();
        }


    }
}