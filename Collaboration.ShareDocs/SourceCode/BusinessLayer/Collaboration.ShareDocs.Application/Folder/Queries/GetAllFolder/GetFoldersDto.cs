using Application.Common.Mapping;
using Application.Project.Queries.GetProjectByKeyWord;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Collaboration.ShareDocs.Persistence.Entities;

namespace Application.Folder.Queries.GetAllFolder
{
    public class GetFoldersDto:IMapForm<Folder>
    {
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<File> Files { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Folder, GetFoldersDto>();
        }
    }
}
