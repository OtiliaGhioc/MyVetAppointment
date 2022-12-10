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
            var appoitnmentDto = new AppointmentCreateDto(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "title", "description", "type");

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

        [Fact]
        public async Task WhenGetByNonExistingId_ThenReturnNotFound()
        {
            var existingAppointment = "f75c2263-19ff-4489-9793-2ebfde3c1845";
            //Act
            var appointmentResponse = await HttpClient.GetAsync($"api/appointments/{Guid.Parse(existingAppointment)}");
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

    }
}
