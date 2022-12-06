using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using VetAppointment.WebAPI.Controllers;

namespace VetAppointment.Tests.ITs
{
    public class BaseDrugsIntegrationTests
    {
        private DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data Source = MyTests.db").Options;
        private DatabaseContext databaseContext;
        protected HttpClient HttpClient { get; private set; }

        protected BaseDrugsIntegrationTests()
        {
            var application = new WebApplicationFactory<DrugsController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            databaseContext = new DatabaseContext(options);
            //CleanDatabases();
        }
        protected void CleanDatabases()
        {
            var databaseContext = new DatabaseContext();
            databaseContext.Appointments.RemoveRange(databaseContext.Appointments.ToList());
            databaseContext.Users.RemoveRange(databaseContext.Users.ToList());
            databaseContext.Offices.RemoveRange(databaseContext.Offices.ToList());
            databaseContext.MedicalEntries.RemoveRange(databaseContext.MedicalEntries.ToList());
            databaseContext.Prescriptions.RemoveRange(databaseContext.Prescriptions.ToList());
            databaseContext.Drugs.RemoveRange(databaseContext.Drugs.ToList());
            databaseContext.DrugStocks.RemoveRange(databaseContext.DrugStocks.ToList());
            databaseContext.BillingEntries.RemoveRange(databaseContext.BillingEntries.ToList());
            databaseContext.PrescriptionDrugs.RemoveRange(databaseContext.PrescriptionDrugs.ToList());
            databaseContext.SaveChanges();
            //databaseContext.Database.EnsureDeleted();
        }
    }
}
