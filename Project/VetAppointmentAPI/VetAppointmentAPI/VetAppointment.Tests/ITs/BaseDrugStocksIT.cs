using Microsoft.AspNetCore.Mvc.Testing;
using VetAppointment.WebAPI.Controllers;

namespace VetAppointment.Tests.ITs
{
    public class BaseDrugStocksIT
    {
        private DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data Source = MyTests.db").Options;
        private DatabaseContext databaseContext;

        protected HttpClient HttpClient { get; private set; }

        //private DatabaseContext databaseContext;

        protected BaseDrugStocksIT()
        {
            var application = new WebApplicationFactory<DrugStocksController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            databaseContext = new DatabaseContext(options);
        }
        protected void CleanDatabases()
        {
            //var databaseContext = new DatabaseContext();
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
