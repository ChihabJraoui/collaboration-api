﻿using Collaboration.ShareDocs.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Notification
    {
        
        public Guid NotificationId { get; set; }
        public string Text { get; set; }

        //[JsonIgnore]
        //public virtual List<NotificationApplicationUser> NotificationApplicationUsers { get; set; }
        public Category Category { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
