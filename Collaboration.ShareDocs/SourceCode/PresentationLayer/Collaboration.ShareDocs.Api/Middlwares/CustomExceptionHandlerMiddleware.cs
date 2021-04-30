using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Collaboration.ShareDocs.Api.Middlwares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiResponseDetails apiResponse = null;
            switch (exception)
            {
                case ValidationException validationException:
                    apiResponse = ApiCustomResponse.ValidationErrors(validationException.Errors);
                    break;
                case BadRequestException badRequestException:
                    apiResponse = ApiCustomResponse.IncompleteRequest( badRequestException.Message);
                    break;
                case NotFoundException notFoundException:
                    apiResponse = ApiCustomResponse.NotFound(notFoundException.Message);
                    break;
                default:

                    apiResponse = new ApiResponseDetails
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        StatusName = HttpStatusCode.InternalServerError.ToString(),
                        Message = exception.Message
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = apiResponse.StatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
