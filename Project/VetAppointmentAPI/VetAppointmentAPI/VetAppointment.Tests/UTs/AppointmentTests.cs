using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void TestAdd(AppointmentRepository officeRepo, Appointment office)
        {
            Appointment added = officeRepo.Add(office);
            officeRepo.SaveChanges();
            Assert.AreEqual(office, added);
        }

        private void TestGet(AppointmentRepository officeRepo, Appointment office)
        {
            Assert.AreEqual(office, officeRepo.Get(office.AppointmentId));
        }

        private void TestAll(AppointmentRepository officeRepo, Appointment office)
        {
            var allOffices = officeRepo.All();
            bool check = false;

            if (allOffices.Contains<Appointment>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestFind(AppointmentRepository officeRepo, Appointment office, Expression<Func<Appointment, bool>> predicate)
        {
            var foundOffices = officeRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Appointment>(office))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestDelete(AppointmentRepository officeRepo, Appointment office)
        {
            officeRepo.Delete(office);
            officeRepo.SaveChanges();
            Assert.IsNull(officeRepo.Get(office.AppointmentId));
        }
    }
}
