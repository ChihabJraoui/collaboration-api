using Collaboration.ShareDocs.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class IndividualChat
    {
        public Guid  Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public ApplicationUser From { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser To { get; set; }
        public IndividualChat()
        {

        }

    }
}
