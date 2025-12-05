using EmpTrack.Application.Features.Auth.Mapping;
using EmpTrack.Application.Features.Departments.Mapping;
using EmpTrack.Application.Features.Employees.Mapping;
using EmpTrack.Application.Features.Titles.Mapping;
using EmpTrack.Application.Marker;
using Microsoft.Extensions.DependencyInjection;

namespace EmpTrack.Application.DependencyInjection
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<TitleProfile>();
                cfg.AddProfile<UserProfile>();
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
            });

            return services;
        }
    }
}
