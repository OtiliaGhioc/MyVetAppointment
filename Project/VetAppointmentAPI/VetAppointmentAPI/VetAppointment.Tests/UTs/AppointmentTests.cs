using Microsoft.EntityFrameworkCore;

namespace VetAppointment.Tests.UTs
{
    public class AppointmentTests
    {
        [Fact]
        public void TestOfficeInfo()
        {
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment office = new Appointment(user,user2,DateTime.Now,"title","descr","type");
            Assert.AreEqual("title", office.Title);
            Assert.AreEqual("descr", office.Description);
            Assert.AreEqual("type", office.Type);
        }

        [Fact]
        public void TestOfficeRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            AppointmentRepository officeRepo = new AppointmentRepository(testDb);
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment office = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");

            TestAdd(officeRepo, office);
            TestGet(officeRepo, office);
            TestAll(officeRepo, office);

            Expression<Func<Appointment, bool>> predicate = u => u.Description.Equals(office.Description);
            TestFind(officeRepo, office, predicate);

            TestDelete(officeRepo, office);
        }

        private async void TestAdd(AppointmentRepository officeRepo, Appointment office)
        {
            Appointment added = await officeRepo.Add(office);
            await officeRepo.SaveChanges();
            Assert.AreEqual(office, added);
        }

        private async void TestGet(AppointmentRepository officeRepo, Appointment office)
        {
            Assert.AreEqual(office, await officeRepo.Get(office.AppointmentId));
        }

        private async void TestAll(AppointmentRepository officeRepo, Appointment office)
        {
            var allOffices = await officeRepo.All();
            bool check = false;

            if (allOffices.Contains<Appointment>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestFind(AppointmentRepository officeRepo, Appointment office, Expression<Func<Appointment, bool>> predicate)
        {
            var foundOffices = await officeRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Appointment>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestDelete(AppointmentRepository officeRepo, Appointment office)
        {
            await officeRepo.Delete(office);
            await officeRepo.SaveChanges();
            Assert.IsNull(officeRepo.Get(office.AppointmentId));
        }
    }
}
