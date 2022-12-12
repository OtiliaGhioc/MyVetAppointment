using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.Tests.UTs
{
    public class AppontmentEssentialExtendedDtoTests
    {
        [Fact]
        public void TestAppointmentEssentialOnlyDtoInfo()
        {
            User user = new User("name", "pass", true);
            User user2 = new User("name", "pass", true);
            Appointment appointment = new Appointment(user, user2, DateTime.Now, "title", "descr", "type");

            AppontmentEssentialExtendedDto onlyDto = new AppontmentEssentialExtendedDto(appointment, user, user2);

            Assert.AreEqual(user.Username, onlyDto.Appointer);
            Assert.AreEqual(user2.Username, onlyDto.Appointee);
            Assert.AreEqual(appointment.Title, onlyDto.Title);
            Assert.AreEqual(appointment.DueDate.Date.ToString("dd-MMM-yyyy"), onlyDto.DueDate);
            Assert.AreEqual(appointment.Description, onlyDto.Description);
        }
    }
}
