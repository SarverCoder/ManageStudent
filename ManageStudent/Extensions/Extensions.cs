using ManageStudent.Options;
using ManageStudent.Repository;
using ManageStudent.Repository.Interfaces;
using ManageStudent.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ManageStudent.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ManageStudent.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });


            return services;
        }

        public static IServiceCollection ConfigurationJwt(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = "https://localhost:7217";

                    var signInKey = configuration["Jwt:Key"];

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudiences = ["https://localhost:7217"],
                        ValidIssuers = ["https://localhost:7217"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey))
                    };
                });

            return services;

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddMediatR(r => r.RegisterServicesFromAssemblyContaining(typeof(Program)));

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddOptions<JwtSettings>()
                .BindConfiguration("Jwt");

            return services;
        }




    }
}
