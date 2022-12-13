using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace VetAppointment.Tests.ITs
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DatabaseContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));
                if (dbConnectionDescriptor != null)
                    services.Remove(dbConnectionDescriptor);
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });
                services.AddDbContext<DatabaseContext>((container, options) =>
                {
                    //options.UseInMemoryDatabase("InMemoryEmployeeTest");
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                {
                    appContext.Database.EnsureCreated();
                }
            });
        }
    }
}
