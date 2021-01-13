using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SFA.DAS.ApprenticeCommitments.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiGlobalExceptionHandler(this IApplicationBuilder app)
        {
            static async Task Handler(HttpContext context)
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    if (contextFeature.Error is ValidationException ex)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync(CreateErrorResponse(ex.Errors));
                    }
                    else
                    {
                        //Do nothing (will result in a 500 error)
                    }
                }
            }

            app.UseExceptionHandler(appError =>
            {
                appError.Run(Handler);
            });
            return app;
        }

        private static string CreateErrorResponse(IEnumerable<ValidationFailure> errors)
        {
            var errorList = new Dictionary<string, string>();
            foreach (var error in errors)
            {
                errorList.Add(error.PropertyName, error.ErrorMessage);
            }

            return JsonConvert.SerializeObject(errorList, Formatting.Indented);
        }
    }
}
