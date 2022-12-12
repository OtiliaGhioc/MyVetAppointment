namespace VetAppointment.Tests.UTs
{
    public class BillingEntryTests
    {
        [Fact]
        public void TestBillingEntryInfo()
        {
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");
            Prescription prescription = new Prescription("descr");

            BillingEntry billingEntry = new BillingEntry(user, user2, new DateTime(2022, 12, 12), prescription, appointment, 1);

            Assert.AreEqual(1, billingEntry.Price);
            Assert.AreEqual(user.UserId, billingEntry.IssuerId);
            Assert.AreEqual(user2.UserId, billingEntry.CustomerId);
            Assert.AreEqual(new DateTime(2022, 12, 12), billingEntry.DateTime);
            Assert.AreEqual(billingEntry.AppointmentId, billingEntry.AppointmentId);
            Assert.AreEqual(prescription.PrescriptionId, billingEntry.PrescriptionId);
        }

        [Fact]
        public void TestBillingEntryRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            BillingEntryRepository billEntRepo = new BillingEntryRepository(testDb);
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");
            Prescription prescription = new Prescription("descr");

            BillingEntry billingEntry = new BillingEntry(user, user2, new DateTime(2022, 12, 12), prescription, appointment, 1);

            TestAdd(billEntRepo, billingEntry);
            TestGet(billEntRepo, billingEntry);
            TestAll(billEntRepo, billingEntry);

            Expression<Func<BillingEntry, bool>> predicate = u => u.Price.Equals(billingEntry.Price);
            TestFind(billEntRepo, billingEntry, predicate);

            TestDelete(billEntRepo, billingEntry);
        }

        private async void TestAdd(BillingEntryRepository billEntRepo, BillingEntry billingEntry)
        {
            BillingEntry added = await billEntRepo.Add(billingEntry);
            await billEntRepo.SaveChanges();
            Assert.AreEqual(billingEntry, added);
        }

        private async void TestGet(BillingEntryRepository billEntRepo, BillingEntry billingEntry)
        {
            Assert.AreEqual<BillingEntry>(billingEntry, await billEntRepo.Get(billingEntry.BillingEntryId));
        }

        private async void TestAll(BillingEntryRepository billEntRepo, BillingEntry billingEntry)
        {
            var allOffices = await billEntRepo.All();
            bool check = false;

            if (allOffices.Contains<BillingEntry>(billingEntry))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestFind(BillingEntryRepository billEntRepo, BillingEntry billingEntry, Expression<Func<BillingEntry, bool>> predicate)
        {
            var foundAppointments = await billEntRepo.Find(predicate);
            bool check = false;

            if (foundAppointments.Contains<BillingEntry>(billingEntry))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestDelete(BillingEntryRepository billEntRepo, BillingEntry billingEntry)
        {
            await billEntRepo.Delete(billingEntry);
            await billEntRepo.SaveChanges();
            Assert.IsNull(await billEntRepo.Get(billingEntry.AppointmentId));
        }
    }
}
