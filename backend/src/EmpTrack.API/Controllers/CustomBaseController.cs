using EmpTrack.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace EmpTrack.API.Controllers
{
    [ApiController]
    public abstract class CustomBaseController : ControllerBase
    {
        protected IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            return result.ResultCode switch
            {
                ResultCode.Success => Ok(result.Data),
                ResultCode.Created => StatusCode(201, result.Data),
                ResultCode.NoContent => NoContent(),
                ResultCode.BadRequest => BadRequest(result.Errors),
                ResultCode.Unauthorized => Unauthorized(result.Errors),
                ResultCode.NotFound => NotFound(result.Errors),
                ResultCode.Conflict => Conflict(result.Errors),
                _ => StatusCode(500, result.Errors)
            };
        }

        protected IActionResult CreateActionResult(ServiceResult result)
        {
            return result.ResultCode switch
            {
                ResultCode.Success => Ok(),
                ResultCode.Created => StatusCode(201),
                ResultCode.NoContent => NoContent(),
                ResultCode.BadRequest => BadRequest(result.Errors),
                ResultCode.Unauthorized => Unauthorized(result.Errors),
                ResultCode.NotFound => NotFound(result.Errors),
                ResultCode.Conflict => Conflict(result.Errors),
                _ => StatusCode(500, result.Errors)
            };
        }
    }
}
