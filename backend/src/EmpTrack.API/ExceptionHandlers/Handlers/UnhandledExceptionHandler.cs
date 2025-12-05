using EmpTrack.API.ExceptionHandlers.Base;

namespace EmpTrack.API.ExceptionHandlers.Handlers
{
    public sealed class UnhandledExceptionHandler : BaseExceptionHandler
    {
        public override bool CanHandle(Exception exception) => true;
    }
}
