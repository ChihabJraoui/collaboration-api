using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Follow
    { 
        public Guid FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }

        public Guid FollowingId { get; set; }
        public ApplicationUser following { get; set; }


        public Follow(Guid followerId, Guid followingId)
        {
            FollowerId = followerId;
            FollowingId = followingId;
            
        }
        public Follow()
        {

        }
    }
} 