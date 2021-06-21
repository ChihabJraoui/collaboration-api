using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Notifications.Dto
{
    public class NotifDto
    {
        public int NotificationCount { get; set; }
        public List<NotificationDto> Notification { get; set; }
    }
}
