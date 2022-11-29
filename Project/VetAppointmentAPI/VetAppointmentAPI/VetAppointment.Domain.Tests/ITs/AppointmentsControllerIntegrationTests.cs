using System.Net.Http.Json;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.Tests.ITs
{
    public class AppointmentsControllerIntegrationTests : BaseAppointmentsIntegrationTests
    {

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await HttpClient.GetAsync("api/appointments");
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WhenCreateWithUserNotFound_ThenReturnNotFound()
        {
            //Arange
            var appoitnmentDto = new AppointmentCreateDto(Guid.NewGuid(), Guid.NewGuid(), "description", "type");

            //Act

            var appointmentResponse = await HttpClient.PostAsJsonAsync("api/appointments", appoitnmentDto);
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenDeleteNonExistingAppointment_ThenReturnNotFound()
        {
            //Act
            var appointmentResponse = await HttpClient.DeleteAsync($"api/appointments/{Guid.NewGuid()}");
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

    }
}
