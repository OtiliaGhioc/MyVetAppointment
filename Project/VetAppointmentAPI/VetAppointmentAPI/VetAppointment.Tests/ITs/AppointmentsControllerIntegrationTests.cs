using Moq;
using System.Net.Http.Json;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.Controllers;
using VetAppointment.WebAPI.Dtos;
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
        public async Task WhenCreateValid_ThenReturnCreated()
        {
            var idUser1 = "aa77ea8d-6051-4fc4-b4ff-0c6da523be37";
            var idUser2 = "770d2748-5c3b-492c-be15-61f98eb100ce";
            //Arange
            var appoitnmentDto = new AppointmentCreateDto(Guid.Parse(idUser1), Guid.Parse(idUser2), DateTime.Now, "title", "description", "type");

            //Act

            var appointmentResponse = await HttpClient.PostAsJsonAsync("api/appointments", appoitnmentDto);
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
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
        public async Task WhenGetByNonExistingId_ThenReturnOk()
        {
            var existingAppointment = "f75c2263-19ff-4489-9793-2ebfde3c1845";
            //Act
            var appointmentResponse = await HttpClient.GetAsync($"api/appointments/{Guid.Parse(existingAppointment)}");
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
