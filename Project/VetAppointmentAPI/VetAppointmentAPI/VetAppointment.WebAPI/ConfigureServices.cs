using FluentValidation;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;
using VetAppointment.WebAPI.Dtos.UserDto;
using VetAppointment.WebAPI.Dtos;
using VetAppointment.WebAPI.DTOs;
using VetAppointment.WebAPI.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace VetAppointment.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddValidationServices
            (this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateDrugDto>, DrugValidator>();
            services.AddScoped<IValidator<CreateDrugStockDto>, DrugStockValidator>();
            services.AddScoped<IValidator<OfficeDto>, OfficeValidator>();
            services.AddScoped<IValidator<DefaultUserDto>, UserValidator>();
            services.AddScoped<IValidator<AppointmentCreateDto>, AppointmentValidator>();
            services.AddScoped<IValidator<MedicalEntryCreateDto>, MedicalEntryValidator>();
            services.AddScoped<IValidator<BillingEntryDto>, BillingEntryValidator>();
            return services;
        }

        public static IServiceCollection AddAuthenticationServices
            (this IServiceCollection services , IConfiguration configuration)
        {
            SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret") ?? throw new ArgumentNullException(nameof(AddAuthenticationServices))));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(item => item.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("JWT:Issuer"),
                    ValidateAudience = true,
                    ValidAudience = configuration.GetValue<string>("JWT:Audience"),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("medic", policy => policy.RequireClaim(ClaimTypes.Role, "medic"));
                options.AddPolicy("default", policy => policy.RequireClaim(ClaimTypes.Role, "default"));
            });
            return services;
        }

        public static IServiceCollection AddCorsServices
            (this IServiceCollection services, string CustomAllowSpecificOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CustomAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });
            return services;
        }

        public static IServiceCollection AddVersioningServices
            (this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine
                    (
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("X-version"),
                        new MediaTypeApiVersionReader("ver")
                    );
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            return services;
        }

        public static IServiceCollection AddMappingServices
            (this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
