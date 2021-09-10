using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto
{
    public class IndividulChatModelDto
    {
        public List<IndividualChatDto> History { get; set; }
        public string UserName { get; set; }
    }
}
