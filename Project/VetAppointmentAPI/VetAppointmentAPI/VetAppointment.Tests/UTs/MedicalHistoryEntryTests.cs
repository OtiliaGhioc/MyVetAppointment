namespace VetAppointment.Tests.UTs
{
    public class MedicalHistoryEntryTests
    {
        [Fact]
        public void TestMedicalHistoryEntryInfo()
        {
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");
            Prescription prescription = new Prescription("descr");

            MedicalHistoryEntry medicalHistoryEntry = new MedicalHistoryEntry(appointment, prescription);

            Assert.AreEqual(appointment.AppointmentId, medicalHistoryEntry.AppointmentId);
            Assert.AreEqual(prescription.PrescriptionId, medicalHistoryEntry.PrescriptionId);
        }

        [Fact]
        public void TestMedicalHistoryEntryRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            MedicalHistoryEntryRepository medHistRepo = new MedicalHistoryEntryRepository(testDb);
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");
            Prescription prescription = new Prescription("descr");

            MedicalHistoryEntry medicalHistoryEntry = new MedicalHistoryEntry(appointment, prescription);

            TestAdd(medHistRepo, medicalHistoryEntry);
            TestGet(medHistRepo, medicalHistoryEntry);
            TestAll(medHistRepo, medicalHistoryEntry);

            Expression<Func<MedicalHistoryEntry, bool>> predicate = u => u.AppointmentId.Equals(medicalHistoryEntry.AppointmentId);
            TestFind(medHistRepo, medicalHistoryEntry, predicate);

            TestDelete(medHistRepo, medicalHistoryEntry);
        }

        private async void TestAdd(MedicalHistoryEntryRepository medHistRepo, MedicalHistoryEntry medicalHistoryEntry)
        {
            var added = await medHistRepo.Add(medicalHistoryEntry);
            await medHistRepo.SaveChanges();
            Assert.AreEqual(medicalHistoryEntry, added);
        }

        private async void TestGet(MedicalHistoryEntryRepository medHistRepo, MedicalHistoryEntry medicalHistoryEntry)
        {
            Assert.AreEqual<MedicalHistoryEntry>(medicalHistoryEntry, await medHistRepo.Get(medicalHistoryEntry.MedicalHistoryEntryId));
        }

        private async void TestAll(MedicalHistoryEntryRepository medHistRepo, MedicalHistoryEntry medicalHistoryEntry)
        {
            var allMedHistEntries = await medHistRepo.All();
            bool check = false;

            if (allMedHistEntries.Contains<MedicalHistoryEntry>(medicalHistoryEntry))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestFind(MedicalHistoryEntryRepository medHistRepo, MedicalHistoryEntry medicalHistoryEntry, Expression<Func<MedicalHistoryEntry, bool>> predicate)
        {
            var foundMedHistEntries = await medHistRepo.Find(predicate);
            bool check = false;

            if (foundMedHistEntries.Contains<MedicalHistoryEntry>(medicalHistoryEntry))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestDelete(MedicalHistoryEntryRepository medHistRepo, MedicalHistoryEntry medicalHistoryEntry)
        {
            await medHistRepo.Delete(medicalHistoryEntry);
            await medHistRepo.SaveChanges();
            Assert.IsNull(await medHistRepo.Get(medicalHistoryEntry.MedicalHistoryEntryId));
        }
    }
}
