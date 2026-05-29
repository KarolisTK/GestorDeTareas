using GestorDeTareas.Exceptions;
using System.Text.Json;

namespace GestorDeTareas.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await EscribirRespuesta(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ForbiddenException ex)
            {
                await EscribirRespuesta(context, StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (ConflictException ex)
            {
                await EscribirRespuesta(context, StatusCodes.Status409Conflict, ex.Message);
            }
            catch(FriendException ex)
            {
                await EscribirRespuesta(context, 455, ex.Message);
            }
            catch (SolicitudPendienteException ex)
            {
                await EscribirRespuesta(context, 456, ex.Message);
            }
            catch (Exception ex)
            {
                await EscribirRespuesta(context, StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }

        }

        private static async Task EscribirRespuesta(HttpContext context, int statusCode, string mensaje)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var body = JsonSerializer.Serialize(new { error = mensaje });
            await context.Response.WriteAsync(body);
        }
    }
}
