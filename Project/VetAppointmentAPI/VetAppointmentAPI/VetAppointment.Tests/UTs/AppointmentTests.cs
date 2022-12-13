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

        private static async void TestAdd(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            Appointment added = await appointmentRepo.Add(appointment);
            await appointmentRepo.SaveChanges();
            Assert.AreEqual(appointment, added);
        }

        private static async void TestGet(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            Assert.AreEqual<Appointment>(appointment, await appointmentRepo.Get(appointment.AppointmentId));
        }

        private static async void TestAll(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            var allOffices = await appointmentRepo.All();
            bool check = false;

            if (allOffices.Contains<Appointment>(appointment))
                check = true;

            Assert.IsTrue(check);
        }

        private static async void TestFind(AppointmentRepository appointmentRepo, Appointment appointment, Expression<Func<Appointment, bool>> predicate)
        {
            var foundAppointments = await appointmentRepo.Find(predicate);
            bool check = false;

            if (foundAppointments.Contains<Appointment>(appointment))
                check = true;

            Assert.IsTrue(check);
        }

        private static async void TestDelete(AppointmentRepository appointmentRepo, Appointment appointment)
        {
            await appointmentRepo.Delete(appointment);
            await appointmentRepo.SaveChanges();
            Assert.IsNull(await appointmentRepo.Get(appointment.AppointmentId));
        }
    }
}
