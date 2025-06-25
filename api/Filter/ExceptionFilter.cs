using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using service.Commons.Exceptions;
using api.Dtos;

namespace api.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        //private readonly Logger _logger;
        //public ExceptionFilter(Logger logger) {
        //    _logger = logger;
        //}

        public ExceptionFilter()
        {
            
        }
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,

            };

            string message = context.Exception.Message;

            //_logger.Error(statusCode+ ": "+ message);

            if (statusCode == 500)
            {
                message = "Ha ocurrido un error en el sistema, se ha notificado al administrador.";
            }

            context.Result = new ObjectResult(
                new ResponseDto(message, statusCode, false, null)
            )
            {
                StatusCode = statusCode,
            };

        }
    }
}
