using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Follows.Dto
{
    public class FollowingDto
    {
        public int FollowingCount { get; set; }
        public List<ICollection<FollowDto>> Following { get; set; }

    }
}
