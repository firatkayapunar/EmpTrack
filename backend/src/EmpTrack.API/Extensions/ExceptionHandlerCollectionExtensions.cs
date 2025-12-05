using EmpTrack.API.ExceptionHandlers.Handlers;

namespace EmpTrack.API.Extensions
{
    public static class ExceptionHandlerCollectionExtensions
    {
        public static IServiceCollection AddCustomExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<SqlExceptionHandler>();
            services.AddExceptionHandler<TimeoutExceptionHandler>();
            services.AddExceptionHandler<UnhandledExceptionHandler>();

            return services;
        }
    }
}
