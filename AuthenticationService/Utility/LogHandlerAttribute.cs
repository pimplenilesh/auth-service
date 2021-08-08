using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AuthenticationService.API.Utility
{
    public class LogHandlerAttribute : ActionFilterAttribute
    {
        private NLog.Logger _logger;

        public LogHandlerAttribute()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var host = context.HttpContext.Request.Host;
            var requestTime = DateTime.Now;

            _logger.Log(NLog.LogLevel.Debug, $"ActionName: {action}");
            _logger.Log(NLog.LogLevel.Debug, $"Request Time: {requestTime}");
            _logger.Log(NLog.LogLevel.Debug, $"Host Name: {host}");

            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var requestCompletedTime = DateTime.Now;

            _logger.Log(NLog.LogLevel.Debug, $"Request completed time: {requestCompletedTime}");
            base.OnActionExecuted(context);
        }
    }
}
