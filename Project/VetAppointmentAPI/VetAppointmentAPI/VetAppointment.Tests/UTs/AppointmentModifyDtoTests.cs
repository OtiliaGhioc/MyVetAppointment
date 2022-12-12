using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.Tests.UTs
{
    public class AppointmentModifyDtoTests
    {
        [Fact]
        public void TestAppointmentModifyDtoInfo()
        {
            DateTime dateTime = new DateTime(2022, 12, 12);
            AppointmentModifyDto appointmentMod = new AppointmentModifyDto(dateTime, "descr", "title", "type", false);

            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");

            Assert.AreEqual(dateTime, appointmentMod.DueDate);
            Assert.AreEqual("descr", appointmentMod.Description);
            Assert.AreEqual("title", appointmentMod.Title);
            Assert.AreEqual("type", appointmentMod.Type);
            Assert.IsFalse(appointmentMod.IsExpired);

            Appointment result = appointmentMod.ApplyModificationsToModel(appointment);

            Assert.AreEqual(appointmentMod.DueDate, result.DueDate);
            Assert.AreEqual(appointmentMod.Description, result.Description);
            Assert.AreEqual(appointmentMod.Title, result.Title);
            Assert.AreEqual(appointmentMod.Type, result.Type);
            Assert.IsFalse(result.IsExpired);
        }
    }
}
