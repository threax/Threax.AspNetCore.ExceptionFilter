using Threax.AspNetCore.ExceptionToJson;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// This attribute will test the model state to see if it is valid. If it is not valid it will
    /// throw a ValidationException. This will be handled by ApiExceptionFilterAttribute if it has been
    /// configured.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =false)]
    public class AutoValidateAttribute : Attribute, IActionFilter
    {
        public String message;

        public AutoValidateAttribute()
        {
            message = "Request not valid.";
        }

        public AutoValidateAttribute(String message)
        {
            this.message = message;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ValidationException(message);
            }
        }
    }
}
