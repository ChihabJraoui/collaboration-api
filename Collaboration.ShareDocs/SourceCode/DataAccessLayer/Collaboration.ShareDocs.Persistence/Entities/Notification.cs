using Collaboration.ShareDocs.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Notification
    {
        
        public Guid NotificationId { get; set; }
        public string Text { get; set; }
        public List<NotificationApplicationUser> NotificationApplicationUsers { get; set; }
    }
}
