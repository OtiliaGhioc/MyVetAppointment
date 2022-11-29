﻿using Microsoft.AspNetCore.Mvc.Testing;
using VetAppointment.Infrastructure.Context;
using VetAppointment.WebAPI.Controllers;

namespace VetAppointment.Tests.ITs
{
    public class BaseUsersIntegrationTests
    {

        protected HttpClient HttpClient { get; private set; }

        //private DatabaseContext databaseContext;

        protected BaseUsersIntegrationTests()
        {
            var application = new WebApplicationFactory<UsersController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            //databaseContext = new DatabaseContext(options);
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