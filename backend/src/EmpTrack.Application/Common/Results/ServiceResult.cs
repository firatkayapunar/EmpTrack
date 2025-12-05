using System.Text.Json.Serialization;

namespace EmpTrack.Application.Common.Results
{
    public abstract class BaseServiceResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; init; }

        [JsonIgnore]
        public ResultCode ResultCode { get; init; }

        [JsonIgnore]
        public bool IsSuccess =>
            ResultCode == ResultCode.Success ||
            ResultCode == ResultCode.Created ||
            ResultCode == ResultCode.NoContent;

    }

    public class ServiceResult : BaseServiceResult
    {
        private ServiceResult() { }

        public static ServiceResult Success(ResultCode resultCode = ResultCode.Success) => new()
        {
            ResultCode = resultCode
        };

        public static ServiceResult Fail(ResultCode resultCode, params string[] errors) => new()
        {
            ResultCode = resultCode,
            Errors = errors
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .ToList()
        };

        public static ServiceResult Fail(ResultCode resultCode, List<string> errors) => new()
        {
            ResultCode = resultCode,
            Errors = errors
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .ToList()
        };
    }

    // ServiceResult<T>, BaseServiceResult’ı extend eden, Data taşıyan generic sınıftır.
    public class ServiceResult<T> : BaseServiceResult
    {
        public T? Data { get; init; }

        private ServiceResult() { }

        public static ServiceResult<T> Success(T data, ResultCode resultCode = ResultCode.Success) => new()
        {
            Data = data,
            ResultCode = resultCode
        };

        public static ServiceResult<T> Fail(ResultCode resultCode, params string[] errors) => new()
        {
            ResultCode = resultCode,
            Errors = errors
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .ToList()
        };

        public static ServiceResult<T> Fail(ResultCode resultCode, List<string> errors) => new()
        {
            ResultCode = resultCode,
            Errors = errors
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .ToList()
        };
    }
}
