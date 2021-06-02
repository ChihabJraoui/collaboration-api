using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Files.Dto
{
    public class FileDto: IMapForm<File>
    {
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }

        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public string Extension { get; set; }
        //TODO add FolderId prop
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<File, FileDto>();
        }
    
    }
}
