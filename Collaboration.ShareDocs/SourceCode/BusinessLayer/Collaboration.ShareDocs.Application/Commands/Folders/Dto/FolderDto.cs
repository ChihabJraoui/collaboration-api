using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Folders.Dto
{
    public class FolderDto: IMapForm<Folder>
    {
        public Guid FolderId { get; set; }
        public string Name { get; set; }
        public  ICollection<File> Files { get; set; }
        public DateTime Created { get; set; }
        public string  CreatedBy { get; set; }
        //TODO add FolderId prop
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Folder, FolderDto>();
        }
    }
}
