using Microsoft.EntityFrameworkCore;

namespace VetAppointment.Tests.UTs
{
    public class DrugTests
    {
        [Fact]
        public void TestDrugInfo()
        {
            Drug drug = new Drug("title",5);
            Assert.AreEqual("title", drug.Title);
            Assert.AreEqual(5, drug.Price);
        }

        [Fact]
        public void TestOfficeRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            DrugRepository officeRepo = new DrugRepository(testDb);
            Drug office = new Drug("title", 5);

            TestAdd(officeRepo, office);
            TestGet(officeRepo, office);
            TestAll(officeRepo, office);

            Expression<Func<Drug, bool>> predicate = u => u.Title.Equals(office.Title);
            TestFind(officeRepo, office, predicate);

            TestDelete(officeRepo, office);
        }

        private void TestAdd(DrugRepository officeRepo, Drug office)
        {
            Drug added = officeRepo.Add(office);
            officeRepo.SaveChanges();
            Assert.AreEqual(office, added);
        }

        private void TestGet(DrugRepository officeRepo, Drug office)
        {
            Assert.AreEqual(office, officeRepo.Get(office.DrugId));
        }

        private void TestAll(DrugRepository officeRepo, Drug office)
        {
            var allOffices = officeRepo.All();
            bool check = false;

            if (allOffices.Contains<Drug>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestFind(DrugRepository officeRepo, Drug office, Expression<Func<Drug, bool>> predicate)
        {
            var foundOffices = officeRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Drug>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestDelete(DrugRepository officeRepo, Drug office)
        {
            officeRepo.Delete(office);
            officeRepo.SaveChanges();
            Assert.IsNull(officeRepo.Get(office.DrugId));
        }
    }
}
