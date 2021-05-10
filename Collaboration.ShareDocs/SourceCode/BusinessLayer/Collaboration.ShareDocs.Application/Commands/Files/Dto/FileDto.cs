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
        public Guid FolderId { get; set; }
        public string Name { get; set; }
        //TODO add FolderId prop
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Folder, FileDto>();
        }
    
    }
}
