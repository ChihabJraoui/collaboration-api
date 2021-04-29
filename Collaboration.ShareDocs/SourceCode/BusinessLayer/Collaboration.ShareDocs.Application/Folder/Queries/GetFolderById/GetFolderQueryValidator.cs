using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Queries
{
    public class GetFolderQueryValidator : AbstractValidator< GetFolderQuery>
    {
        public GetFolderQueryValidator()
        {
            this.RuleFor(p => p.FolderId).NotEmpty().WithMessage("Id is required");
        }
    }
}
