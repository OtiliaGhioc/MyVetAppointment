using Microsoft.EntityFrameworkCore;

namespace VetAppointment.Tests.UTs
{
    public class AppointmentTests
    {
        [Fact]
        public void TestAppointmentInfo()
        {
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user,user2,DateTime.Now,"title","descr","type");
            Assert.AreEqual("title", appointment.Title);
            Assert.AreEqual("descr", appointment.Description);
            Assert.AreEqual("type", appointment.Type);
        }

        [Fact]
        public void TestAppointmentRepository()
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite("Data Source = MyTests.db").Options;
            DatabaseContext testDb = new DatabaseContext(options);
            AppointmentRepository appointmentRepo = new AppointmentRepository(testDb);
            User user = new User("name", "password");
            User user2 = new User("name", "password");
            Appointment appointment = new Appointment(user, user2, DateTime.Today.AddDays(1), "title", "description", "type");

            TestAdd(appointmentRepo, appointment);
            TestGet(appointmentRepo, appointment);
            TestAll(appointmentRepo, appointment);

            Expression<Func<Appointment, bool>> predicate = u => u.Description.Equals(appointment.Description);
            TestFind(appointmentRepo, appointment, predicate);

            TestDelete(appointmentRepo, appointment);
        }

        private async void TestAdd(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            Appointment added = await appointmentRepo.Add(appointment);
            await appointmentRepo.SaveChanges();
            Assert.AreEqual(appointment, added);
        }

        private async void TestGet(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            Assert.AreEqual(appointment, await appointmentRepo.Get(appointment.AppointmentId));
        }

        private async void TestAll(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            var allOffices = await appointmentRepo.All();
            bool check = false;

            if (allOffices.Contains<Appointment>(appointment))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestFind(AppointmentRepository appointmentRepo, Appointment appointment, Expression<Func<Appointment, bool>> predicate)
        {
            var foundOffices = await appointmentRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Appointment>(appointment))
                check = true;

            Assert.IsTrue(check);
        }

        private async void TestDelete(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            await appointmentRepo.Delete(appointment);
            await appointmentRepo.SaveChanges();
            Assert.IsNotNull(appointmentRepo.Get(appointment.AppointmentId));
        }
    }
}
