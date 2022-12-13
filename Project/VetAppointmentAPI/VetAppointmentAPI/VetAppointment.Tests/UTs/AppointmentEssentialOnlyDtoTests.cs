using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.Tests.UTs
{
    public class AppointmentEssentialOnlyDtoTests
    {
        [Fact]
        public void TestAppointmentEssentialOnlyDtoInfo()
        {
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");

            AppointmentEssentialOnlyDto onlyDto = new AppointmentEssentialOnlyDto(appointment, user);

            Assert.AreEqual(user.Username, onlyDto.Appointer);
            Assert.AreEqual(appointment.Title, onlyDto.Title);
            Assert.AreEqual(appointment.DueDate.ToString("dd-MMM-yyyy"), onlyDto.DueDate);
            Assert.AreEqual(appointment.DueDate.ToString("HH:mm"), onlyDto.DueTime);
        }
    }
}
