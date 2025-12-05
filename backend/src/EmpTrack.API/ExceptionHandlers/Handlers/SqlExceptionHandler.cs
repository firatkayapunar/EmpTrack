using EmpTrack.API.ExceptionHandlers.Base;
using Microsoft.Data.Sqlite;

namespace EmpTrack.API.ExceptionHandlers.Handlers
{
    public sealed class SqlExceptionHandler : BaseExceptionHandler
    {
        public override bool CanHandle(Exception exception) => exception is SqliteException;
    }
}
