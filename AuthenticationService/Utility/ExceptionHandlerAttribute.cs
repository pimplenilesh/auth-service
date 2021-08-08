using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService.API.Utility
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var message = context.Exception.Message;

            if (exceptionType == typeof(Exceptions.UserNotCreatedException))
            {
                context.Result = new ConflictObjectResult(message);
            }
            else if (exceptionType == typeof(Exceptions.UserNotFoundException))
            {
                context.Result = new NotFoundObjectResult(message);
            }
            else
            {
                var result = new StatusCodeResult(500);
                context.Result = result;
            }

            base.OnException(context);
        }
    }
}
