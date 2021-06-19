using Collaboration.ShareDocs.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class NotificationApplicationUser
    {
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsRead { get; set; } = false;
        
    }
}
