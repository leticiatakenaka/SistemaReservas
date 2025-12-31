using SistemaReservas.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace SistemaReservas.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (exception)
            {
                case DomainException domainEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Errors.Add(domainEx.Message);
                    break;

                default:
                    _logger.LogError(exception, "Erro não tratado ocorreu."); 
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Errors.Add("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
                    break;
            }

            var jsonResult = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResult);
        }
    }

    public class ErrorResponse
    {
        public List<string> Errors { get; set; } = new List<string>();
    }
}