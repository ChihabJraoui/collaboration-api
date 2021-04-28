using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Follower
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }

        public Guid MemberId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFollowing { get; set; }
        
    }
}
