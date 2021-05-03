using System.Collections.Generic;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Newtonsoft.Json;

namespace Collaboration.ShareDocs.Application.Common.Response
{
    public class ApiResponseDetails
    {
        public int StatusCode { get; set; }

        public string StatusName { get; set; }

        public object Object { get; set; }

        public string Message { get; set; }

        public List<Error> Errors { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
