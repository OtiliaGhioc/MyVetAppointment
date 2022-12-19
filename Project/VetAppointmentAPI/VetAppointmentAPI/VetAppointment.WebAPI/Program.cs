using VetAppointment.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// cors
var CustomAllowSpecificOrigins = "_customAllowSpecificOrigins";
builder.Services.AddCorsServices(CustomAllowSpecificOrigins);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddVersioningServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddValidationServices();
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddMappingServices();

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

// PS > dotnet C:\Users\{YourUser}\.nuget\packages\reportgenerator\5.1.12\tools\net7.0\ReportGenerator.dll -reports:.\VetAppointment.Tests\TestResults\{CoverageFolderName}\coverage.cobertura.xml -targetdir:.\VetAppointment.Tests\TestResults\{CoverageFolderName}\report
// PS > dotnet C:\Users\Bogdan\.nuget\packages\reportgenerator\5.1.12\tools\net7.0\ReportGenerator.dll -reports:.\VetAppointment.Tests\TestResults\a538af64-0b38-4801-aa83-ba4f4d422c03\coverage.cobertura.xml -targetdir:.\VetAppointment.Tests\TestResults\a538af64-0b38-4801-aa83-ba4f4d422c03\report

// Open either index.html in report folder
