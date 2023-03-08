using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Handlers.Exceptions
{
    public class ExceptionHandler
    {
        public void LogException(Exception ex, ILogger logger)
        {
            if (ex == null)
                return;

            logger.LogError(ex.Message);
            LogException(ex.InnerException, logger);
        }

        public static async Task<T> Handle<T>(Func<Task<T>> action, ILogger logger)
        {
            try
            {
                return await action();
            }
            catch (AppException ex)
            {

                throw new Exception(ex.Message);
            }
            catch (Exception ex) when (ex is not AppException)
            {

                throw new Exception(ExceptionCodes.DefaultError);
            }
        }
    }
}