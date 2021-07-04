using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Follow
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid FollowingId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public ApplicationUser Following { get; set; }

        /// <summary>
        /// Follower ID
        /// </summary>
        public Guid FollowerId { get; set; }

        /// <summary>
        /// Follower
        /// </summary>
        public ApplicationUser Follower { get; set; }

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