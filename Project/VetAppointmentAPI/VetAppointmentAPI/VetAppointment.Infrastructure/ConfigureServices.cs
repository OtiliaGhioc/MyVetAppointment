using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Impl;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;

namespace VetAppointment.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite(
                configuration.GetConnectionString("VetAppointmentDb"),
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IBillingEntryRepository, BillingEntryRepository>();
            services.AddScoped<IDrugRepository, DrugRepository>();
            services.AddScoped<IDrugStockRepository, DrugStockRepository>();
            services.AddScoped<IMedicalHistoryEntryRepository, MedicalHistoryEntryRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
