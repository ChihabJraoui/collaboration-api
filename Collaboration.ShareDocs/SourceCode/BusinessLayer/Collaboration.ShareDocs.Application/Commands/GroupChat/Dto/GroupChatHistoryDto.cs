using Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat.Dto
{
    public class GroupChatHistoryDto
    {
        public IndividualChatDto Messages { get; set; }
        public bool replay { get; set; }
    }
}
