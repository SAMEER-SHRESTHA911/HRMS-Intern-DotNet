using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Http.Interface;
using User.Domain.Services.Interface;
using User.Infrastructure.Http.Implementation;
using User.Infrastructure.Repository;
using User.Infrastructure.Service.Implementation;

namespace User.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(o => o.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]), ServiceLifetime.Scoped);

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<IUserServiceFactory, UserServiceFactory>();
            services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>)); // Register the generic repository
            services.AddScoped<IHttpAttendanceLeaveService, HttpAttendanceLeaveService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; //for development
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
            return services;
        }
    }
}
