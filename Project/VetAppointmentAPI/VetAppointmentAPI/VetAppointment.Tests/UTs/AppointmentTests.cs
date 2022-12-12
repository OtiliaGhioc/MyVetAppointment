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
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");

            TestAdd(appointmentRepo, appointment);
            TestGet(appointmentRepo, appointment);
            TestAll(appointmentRepo, appointment);

            Expression<Func<Appointment, bool>> predicate = u => u.Description.Equals(appointment.Description);
            TestFind(appointmentRepo, appointment, predicate);

            TestDelete(appointmentRepo, appointment);
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

            if (allOffices.Contains<Appointment>(appointment))
                check = true;

            Assert.IsTrue(check);
        }

        private void TestFind(AppointmentRepository officeRepo, Appointment office, Expression<Func<Appointment, bool>> predicate)
        {
            var foundOffices = officeRepo.Find(predicate);
            bool check = false;

            if (foundOffices.Contains<Appointment>(appointment))
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
