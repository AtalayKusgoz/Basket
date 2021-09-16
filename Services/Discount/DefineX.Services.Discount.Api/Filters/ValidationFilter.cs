using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DefineX.Services.Discount.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                List<string> errors = new List<string>();
                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        errors.Add(subError);
                    }
                }
                context.Result = new BadRequestObjectResult(Response<ErrorResultModelDto>.Fail(errors, (int)HttpStatusCode.InternalServerError));
                return;
            }
            await next();
        }
    }
}
