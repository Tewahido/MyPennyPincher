using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API.CustomExceptionMiddleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate nextDelegate, ILogger<CustomExceptionMiddleware> logger)
        {
            _nextDelegate = nextDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _nextDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";

                var response = new ErrorResponse();

                context.Response.StatusCode = ex switch
                {
                    UserAlreadyExistsException => StatusCodes.Status409Conflict,
                    InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                    InvalidRefreshTokenException => StatusCodes.Status401Unauthorized,
                    ExpensesNotFoundException => StatusCodes.Status404NotFound,
                    ExpenseNotFoundException => StatusCodes.Status404NotFound,
                    IncomesNotFoundException => StatusCodes.Status404NotFound,
                    IncomeNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                response.StatusCode = context.Response.StatusCode;
                response.Message = ex.Message;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
