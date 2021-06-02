using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Files.Dto
{
    public class TimeLineDto
    { 
        public UserProfileDto Author { get; set; }  
        public FileDto File { get; set; }
    }
}
