using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Web.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Response = context.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, context.ModelState);
            }
        }
    }
}
