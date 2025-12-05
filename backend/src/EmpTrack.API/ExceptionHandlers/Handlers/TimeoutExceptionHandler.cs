using EmpTrack.API.ExceptionHandlers.Base;

namespace EmpTrack.API.ExceptionHandlers.Handlers
{
    public sealed class TimeoutExceptionHandler : BaseExceptionHandler
    {
        public override bool CanHandle(Exception exception) => exception is TimeoutException;
    }
}
