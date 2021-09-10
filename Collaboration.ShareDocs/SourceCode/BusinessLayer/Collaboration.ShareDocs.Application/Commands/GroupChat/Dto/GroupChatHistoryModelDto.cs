using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat.Dto
{
    public class GroupChatHistoryModelDto
    {
        public List<List<GroupChatHistoryDto>> History { get; set; }
        public string Name { get; set; }

    }
}
