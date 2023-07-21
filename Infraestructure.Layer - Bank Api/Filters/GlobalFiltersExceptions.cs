using AplicationDomain.Layer___Bank_Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Infraestructure.Layer___Bank_Api.Filters
{
    public class GlobalFiltersExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(GlobalBusinessExceptions))
            {
                var messageException = (GlobalBusinessExceptions)context.Exception;
                var ExceptionsNewMessage = new
                {
                    Status = 400,
                    Title = "BAD REQUEST",
                    Message = messageException.Message
                };
                var Json = new
                {
                    Error = new[] { ExceptionsNewMessage }
                };

                context.Result = new BadRequestObjectResult(Json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
