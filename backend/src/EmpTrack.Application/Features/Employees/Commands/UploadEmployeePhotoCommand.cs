using EmpTrack.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpTrack.Application.Features.Employees.Commands
{
    public record UploadEmployeePhotoCommand(int EmployeeId, IFormFile Photo) : IRequest<ServiceResult>;
}
