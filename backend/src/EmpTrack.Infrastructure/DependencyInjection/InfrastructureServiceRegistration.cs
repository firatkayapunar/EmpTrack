using EmpTrack.Application.Interfaces.Auth;
using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Infrastructure.Auth;
using EmpTrack.Infrastructure.Persistence;
using EmpTrack.Infrastructure.Persistence.Interceptors;
using EmpTrack.Infrastructure.Persistence.Repositories;
using EmpTrack.Infrastructure.Persistence.Seed;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmpTrack.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var basePath = AppContext.BaseDirectory;
            var dbPath = Path.Combine(basePath, "emptrack.db");
            var connStr = $"Data Source={dbPath};Cache=Shared";

            using var conn = new SqliteConnection(connStr);

            conn.Open();

            using var cmd = conn.CreateCommand();

            cmd.CommandText = "PRAGMA journal_mode = WAL;";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "PRAGMA busy_timeout = 8000;";
            cmd.ExecuteNonQuery();

            services.AddScoped<IDataSeeder, DefaultDataSeeder>();
            services.AddHttpContextAccessor();
            services.AddScoped<AuditDbContextInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlite(connStr);

                options.AddInterceptors(
                    sp.GetRequiredService<AuditDbContextInterceptor>()
                );
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            /*
            AddOptions + Validate: JwtSettings ayarları DI container’a eklenir, uygulama startup sırasında doğrulanır ve hatalı ise API hiç ayağa kalkmaz; böylece JWT güvenliği en başta garanti altına alınır.
            */
            services
                .AddOptions<JwtSettings>()
                .Bind(configuration.GetSection(JwtSettings.SectionName))
                .Validate(settings => !string.IsNullOrWhiteSpace(settings.SecretKey) && settings.SecretKey.Length >= 32, "JWT SecretKey must be at least 32 characters.").Validate(settings => !string.IsNullOrWhiteSpace(settings.Issuer), "JWT Issuer cannot be empty.")
                .Validate(settings => !string.IsNullOrWhiteSpace(settings.Audience), "JWT Audience cannot be empty.")
                .ValidateOnStart();

            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
