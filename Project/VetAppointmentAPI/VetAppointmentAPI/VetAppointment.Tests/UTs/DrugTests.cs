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

            drug.UpdateNameAndPrice("name", 1);
            Assert.AreEqual("name", drug.Title);
            Assert.AreEqual(1, drug.Price);
        }

        [Fact]
        public void TestDrugsRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            DrugRepository drugRepo = new DrugRepository(testDb);
            Drug drug = new Drug("title", 5);

            TestAdd(drugRepo, drug);
            TestGet(drugRepo, drug);
            TestAll(drugRepo, drug);

            Expression<Func<Drug, bool>> predicate = u => u.Title.Equals(drug.Title);
            TestFind(drugRepo, drug, predicate);

            TestDelete(drugRepo, drug);
        }

        private static async void TestAdd(DrugRepository drugRepo, Drug drug)
        {
            var added = await drugRepo.Add(drug);
            await drugRepo.SaveChanges();
            Assert.AreEqual(drug, added);
        }

        private static async void TestGet(DrugRepository drugRepo, Drug drug)
        {
            Assert.AreEqual(drug, await drugRepo.Get(drug.DrugId));
        }

        private static async void TestAll(DrugRepository drugRepo, Drug drug)
        {
            var allDrugs = await drugRepo.All();
            bool check = false;

            if (allDrugs.Contains<Drug>(drug))
                check = true;

            Assert.IsTrue(check);
        }

        private static async void TestFind(DrugRepository officeRepo, Drug office, Expression<Func<Drug, bool>> predicate)
        {
            var foundOffices = await officeRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Drug>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private static async void TestDelete(DrugRepository officeRepo, Drug office)
        {
            officeRepo.Delete(office);
            await officeRepo.SaveChanges();
            Assert.IsNotNull(officeRepo.Get(office.DrugId));
        }
    }
}
