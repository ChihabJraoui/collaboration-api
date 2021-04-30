using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Collaboration.ShareDocs.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<Error>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {

            Errors.AddRange(failures.Select(e => new Error(e.PropertyName, e.ErrorMessage)));
        }
        public List<Error> Errors { get; set; }

    }
}
