
using NLog;
using ViewModels.DataBase;
using ILogger = NLog.ILogger;

namespace OnionArchitecture.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;

        private readonly ILogger _logger = LogManager.GetLogger("logError");

        public ExceptionHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate.Invoke(context);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.Error(ex);
                await context.Response.WriteAsync($" {context.Response.StatusCode} - Error is outside of index");
            }
            catch (ArgumentNullException ex)
            {
                _logger.Error(ex);
                await context.Response.WriteAsync($" {context.Response.StatusCode} - Object is null");
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex);
                 new UpdateResultExtend<int>() { UpdateStatus = SaveStatus.Fail, Message = ex.Message, ID = context.Response.StatusCode };
              //  await context.Response.WriteAsync($" {context.Response.StatusCode} - {ex.Message}");
            }
        }
    }
}
