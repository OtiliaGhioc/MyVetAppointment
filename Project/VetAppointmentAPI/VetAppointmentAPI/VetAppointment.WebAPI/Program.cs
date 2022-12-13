using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using VetAppointment.Application.Repositories.Impl;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;
using VetAppointment.WebAPI.Dtos;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;
using VetAppointment.WebAPI.Dtos.UserDto;
using VetAppointment.WebAPI.DTOs;
using VetAppointment.WebAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("VetAppointmentDb"),
    b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

// cors
var CustomAllowSpecificOrigins = "_customAllowSpecificOrigins";

builder.Services.AddCors(options =>
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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBillingEntryRepository, BillingEntryRepository>();
builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IDrugStockRepository, DrugStockRepository>();
builder.Services.AddScoped<IMedicalHistoryEntryRepository, MedicalHistoryEntryRepository>();
builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IValidator<CreateDrugDto>, DrugValidator>();
builder.Services.AddScoped<IValidator<CreateDrugStockDto>, DrugStockValidator>();
builder.Services.AddScoped<IValidator<OfficeDto>, OfficeValidator>();
builder.Services.AddScoped<IValidator<DefaultUserDto>, UserValidator>();
builder.Services.AddScoped<IValidator<AppointmentCreateDto>, AppointmentValidator>();
builder.Services.AddScoped<IValidator<MedicalEntryCreateDto>, MedicalEntryValidator>();
builder.Services.AddScoped<IValidator<BillingEntryDto>, BillingEntryValidator>();

SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Secret") ?? throw new ArgumentNullException(nameof(args))));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(item => item.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = secretKey
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("medic", policy => policy.RequireClaim(ClaimTypes.Role, "medic"));
    options.AddPolicy("default", policy => policy.RequireClaim(ClaimTypes.Role, "default"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CustomAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

// PS > dotnet test /p:CollectCoverage=true -s.\coverlet.runsettings

// PS > dotnet C:\Users\{YourUser}\.nuget\packages\reportgenerator\5.1.12\tools\net7 .0\ReportGenerator.dll - reports:.\VetAppointment.Tests\TestResults\{CoverageFolderName}\coverage.cobertura.xml - targetdir:.\VetAppointment.Tests\TestResults\{CoverageFolderName}\report
// PS > dotnet C:\Users\Bogdan\.nuget\packages\reportgenerator\5.1.12\tools\net7.0\ReportGenerator.dll -reports:.\VetAppointment.Tests\TestResults\a538af64-0b38-4801-aa83-ba4f4d422c03\coverage.cobertura.xml -targetdir:.\VetAppointment.Tests\TestResults\a538af64-0b38-4801-aa83-ba4f4d422c03\report

// Open either index.html in report folder
