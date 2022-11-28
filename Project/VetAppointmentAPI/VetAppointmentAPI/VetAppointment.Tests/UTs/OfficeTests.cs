namespace VetAppointment.Tests
{
    [TestClass]
    public class OfficeTests
    {
        [TestMethod]
        public void TestOfficeInfo()
        {
            Office office = new Office("addr");
            Assert.AreEqual("addr", office.Address);
        }

        [TestMethod]
        public void TestOfficeRepository()
        {
            DatabaseContext testDb = new DatabaseContext();
            OfficeRepository officeRepo = new OfficeRepository(testDb);
            Office office = new Office("addr");

            TestAdd(officeRepo, office);
            TestGet(officeRepo, office);
            TestAll(officeRepo, office);

            Expression<Func<Office, bool>> predicate = u => u.Address.Equals(office.Address);
            TestFind(officeRepo, office, predicate);

            TestDelete(officeRepo, office);
        }

        private void TestAdd(OfficeRepository officeRepo, Office office)
        {
            Office added = officeRepo.Add(office);
            officeRepo.SaveChanges();
            Assert.AreEqual(office, added);
        }

        private void TestGet(OfficeRepository officeRepo, Office office)
        {
            Assert.AreEqual(office, officeRepo.Get(office.OfficeId));
        }

        private void TestAll(OfficeRepository officeRepo, Office office)
        {
            var allOffices = officeRepo.All();
            bool check = false;

            if (allOffices.Contains<Office>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestFind(OfficeRepository officeRepo, Office office, Expression<Func<Office, bool>> predicate)
        {
            var foundOffices = officeRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Office>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestDelete(OfficeRepository officeRepo, Office office)
        {
            officeRepo.Delete(office);
            officeRepo.SaveChanges();
            Assert.IsNull(officeRepo.Get(office.OfficeId));
        }
    }
}
