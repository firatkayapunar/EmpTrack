using EmpTrack.Application.Common.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace EmpTrack.API.ExceptionHandlers.Base
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        // Her concrete handler hangi exception'ı yakalayacağını burada söyler.
        public abstract bool CanHandle(Exception exception);

        // Exception pipeline’ın çalıştırdığı ana metodu burada tanımlıyoruz.
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Eğer bu handler bu exception türünü yönetmiyorsa pipeline bir sonraki handler'a geçer.
            if (!CanHandle(exception))
                return false;

            // Default response.
            var result = ServiceResult.Fail(ResultCode.InternalError, "An unexpected error occurred.");

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(result);

            await httpContext.Response.WriteAsync(json, cancellationToken);

            // True, bu exception yakalandı ve işleme alındı anlamına gelir.
            return true;
        }
    }
}
