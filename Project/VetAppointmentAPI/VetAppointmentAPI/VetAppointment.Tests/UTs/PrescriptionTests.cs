using VetAppointment.Domain.Helpers;

namespace VetAppointment.Tests.UTs
{
    public class PrescriptionTests
    {
        [Fact]
        public void TestPrescriptionInfo()
        {
            Prescription prescription = new Prescription("descr");
            Assert.AreEqual("descr", prescription.Description);
        }

        [Fact]
        public void TestAddDrugToPrescription()
        {
            Prescription prescription = new Prescription("descr");
            var prescriptionDrug = PrescriptionDrug.CreatePrescriptionDrug(new DrugStock(new Drug("title", 1), 1), 1);

            Assert.AreEqual(Result.Success().IsSuccess, prescription.AddDrugToPrescription(prescriptionDrug.Entity).IsSuccess);
        }

        [Fact]
        public void TestPrescriptionRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            PrescriptionRepository prescriptionRepo = new PrescriptionRepository(testDb);
            Prescription prescription = new Prescription("descr");

            TestAdd(prescriptionRepo, prescription);
            TestGet(prescriptionRepo, prescription);
            TestAll(prescriptionRepo, prescription);

            Expression<Func<Prescription, bool>> predicate = u => u.Description.Equals(prescription.Description);
            TestFind(prescriptionRepo, prescription, predicate);

            TestDelete(prescriptionRepo, prescription);
        }

        private void TestAdd(PrescriptionRepository prescriptionRepo, Prescription prescription)
        {
            Prescription added = prescriptionRepo.Add(prescription);
            prescriptionRepo.SaveChanges();
            Assert.AreEqual(prescription, added);
        }

        private void TestGet(PrescriptionRepository prescriptionRepo, Prescription prescription)
        {
            Assert.AreEqual(prescription, prescriptionRepo.Get(prescription.PrescriptionId));
        }

        private void TestAll(PrescriptionRepository prescriptionRepo, Prescription prescription)
        {
            var allPrescriptions = prescriptionRepo.All();
            bool check = false;

            if (allPrescriptions.Contains<Prescription>(prescription))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestFind(PrescriptionRepository prescriptionRepo, Prescription prescription, Expression<Func<Prescription, bool>> predicate)
        {
            var foundOffices = prescriptionRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Prescription>(prescription))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestDelete(PrescriptionRepository prescriptionRepo, Prescription prescription)
        {
            prescriptionRepo.Delete(prescription);
            prescriptionRepo.SaveChanges();
            Assert.IsNull(prescriptionRepo.Get(prescription.PrescriptionId));
        }
    }
}
