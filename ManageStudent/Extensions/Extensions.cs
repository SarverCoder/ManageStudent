using ManageStudent.Options;
using ManageStudent.Repository;
using ManageStudent.Repository.Interfaces;
using ManageStudent.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ManageStudent.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. \n\r Enter 'Bearer' [space] and then your token in the text input below.\n\r Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, []
                    }
                });
            });

            return services;
        }




    }
}
