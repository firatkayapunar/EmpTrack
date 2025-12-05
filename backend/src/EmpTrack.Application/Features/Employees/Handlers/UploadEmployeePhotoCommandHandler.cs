using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Handlers
{
    public class UploadEmployeePhotoCommandHandler : IRequestHandler<UploadEmployeePhotoCommand, ServiceResult>
    {
        private readonly IEmployeeRepository _repository;

        public UploadEmployeePhotoCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(UploadEmployeePhotoCommand request, CancellationToken cancellationToken)
        {
            // Dosya var mı kontrolü yapıyoruz.
            if (request.Photo is null || request.Photo.Length == 0)
                return ServiceResult.Fail(ResultCode.BadRequest, "Uploaded file is empty.");

            // Dosya uzantısı whitelist kontrolü yapıyoruz.
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(request.Photo.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return ServiceResult.Fail(ResultCode.BadRequest, "Invalid image format. Only JPG, PNG or WEBP allowed.");

            // Dosya boyutu kontrolü (5MB) yapıyoruz.
            const long maxSize = 5 * 1024 * 1024;
            if (request.Photo.Length > maxSize)
                return ServiceResult.Fail(ResultCode.BadRequest, "Image size must be less than 5 MB.");

            var employee = await _repository.GetWithDetailsAsync(request.EmployeeId);

            if (employee is null)
                return ServiceResult.Fail(ResultCode.NotFound, "Employee not found.");

            if (!string.IsNullOrWhiteSpace(employee.PhotoPath))
            {
                var oldFilePath = Path.Combine(
                    "wwwroot",
                    employee.PhotoPath.TrimStart('/')
                );

                if (File.Exists(oldFilePath))
                    File.Delete(oldFilePath);
            }

            // Güvenli unique dosya ismi üretme işlemi yapıyoruz.
            var fileName = $"{Guid.NewGuid()}{extension}";

            // Fiziksel klasör yolu oluşturuyoruz.
            var directory = Path.Combine("wwwroot", "images", "employees");

            // Klasör yoksa oluştur diyoruz.
            Directory.CreateDirectory(directory);

            // Full fiziksel dosya yolu belirliyoruz.
            var fullPath = Path.Combine(directory, fileName);

            // Dosyayı disk üzerine yazma işlemi yapıyoruz.
            using var stream =
                new FileStream(fullPath, FileMode.Create);

            await request.Photo.CopyToAsync(stream, cancellationToken);

            // Veritabanına kaydediyoruz.
            employee.PhotoPath = $"/images/employees/{fileName}";

            _repository.Update(employee);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success();
        }
    }
}
