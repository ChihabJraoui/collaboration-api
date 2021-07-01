using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Resources;

namespace Collaboration.ShareDocs.Application.Common.Response
{
    public static class ApiCustomResponse
    {
        public static ApiResponseDetails ReturnedObject(object dynamicObject = null)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.OK,
                StatusName = HttpStatusCode.OK.ToString(),
                Object     = dynamicObject
            };

            return responseDetails;
        }
        public static ApiResponseDetails IncompleteRequest(string message = null)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                StatusName = HttpStatusCode.BadRequest.ToString(),
                Message    = message
            };

            return responseDetails;
        }

        public static ApiResponseDetails Delete(string entity, string key)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.OK,
                StatusName = HttpStatusCode.OK.ToString(),
                Message = entity + " has been deleted",
            };

            return responseDetails;
        }

        public static ApiResponseDetails DeleteMultiple(List<Guid> deletedEntities,
            Dictionary<Guid, string> notDeletedEntities, int idsCount, bool internalError = false)
        {
            if (deletedEntities.Count == idsCount)
            {
                return new ApiResponseDetails
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    StatusName = HttpStatusCode.OK.ToString(),
                    Message = "All entities were deleted successfully"
                };
            }

            var message = deletedEntities.Count == 0
                ? "No entity were deleted."
                : $"All entities were deleted except ({notDeletedEntities.Count})";

            var statusCode = internalError ? HttpStatusCode.InternalServerError : HttpStatusCode.BadRequest;
            return new ApiResponseDetails
            {
                StatusCode = (int)statusCode,
                StatusName = statusCode.ToString(),
                Message = message,
                Errors = notDeletedEntities.Select(x => new Error
                {
                    Name = x.Key.ToString(),
                    Message = x.Value
                }).ToList()
            };
        }

        public static ApiResponseDetails ExestingMultiple(Guid[] exestingUsers,
            int idsCount, bool internalError = false)
        {
            if (exestingUsers.Length == idsCount)
            {
                return new ApiResponseDetails
                {
                    StatusCode = (int)HttpStatusCode.AlreadyReported,
                    StatusName = HttpStatusCode.AlreadyReported.ToString(),
                    Message = ""
                };
            }

           
            var statusCode = internalError ? HttpStatusCode.InternalServerError : HttpStatusCode.BadRequest;
            return new ApiResponseDetails
            {
                StatusCode = (int)statusCode,
                StatusName = statusCode.ToString(),
                
                
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Message : string</param>
        /// <returns></returns>
        public static ApiResponseDetails NotFound(string message)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                StatusName = HttpStatusCode.NotFound.ToString(),
                Message    = message
            };

            return responseDetails;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName">entityName:String</param>
        /// <param name="id"> entity id :Guid</param>
        /// <returns></returns>
        public static ApiResponseDetails NotFound(string entityName, Guid id)
        {
            var message = string.Format(Resource.Error_NotFound, entityName, id);

            return NotFound(message);
        }

        public static ApiResponseDetails Forbidden( string message = null)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.Forbidden,
                StatusName = HttpStatusCode.Forbidden.ToString(),
                Message    = message
            };

            return responseDetails;
        }

        public static ApiResponseDetails NotValid(string entity, string propertyName, string key)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.NotAcceptable,
                StatusName = HttpStatusCode.NotAcceptable.ToString(),
                Message = entity + ": " + propertyName + " format is not valid",
            };

            return responseDetails;
        }
        public static ApiResponseDetails ValidationErrors(List<Error> errors)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                StatusName = HttpStatusCode.BadRequest.ToString(),
                Message = "Validation Errors",
                Errors = errors
            };

            return responseDetails;
        }
        public static ApiResponseDetails ExestingError(string message)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.AlreadyReported,
                StatusName = HttpStatusCode.AlreadyReported.ToString(),
                Message = message,
                 
            };

            return responseDetails;
        }
        public static ApiResponseDetails ValidationError(Error error)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                StatusName = HttpStatusCode.BadRequest.ToString(),
                Message = "Validation Errors",
                Errors = new List<Error> { error }
            };

            return responseDetails;
        }
        
        public static ApiResponseDetails NotAllowed(string message = null)
        {
            var responseDetails = new ApiResponseDetails
            {
                StatusCode = (int)HttpStatusCode.Forbidden,
                StatusName = HttpStatusCode.Forbidden.ToString(),
                Message = message ?? $"The user is not allowed to perform this action.",
            };

            return responseDetails;
        }
    }
}
