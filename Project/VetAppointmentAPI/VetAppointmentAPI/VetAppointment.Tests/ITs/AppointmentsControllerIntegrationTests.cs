using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;
using VetAppointment.WebAPI.Dtos.UserDto;

namespace VetAppointment.Tests.ITs
{
    public class AppointmentsControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient httpClient;
        private readonly TestingWebAppFactory<Program> factory;
        public AppointmentsControllerIntegrationTests(TestingWebAppFactory<Program> factory)
        {
            this.factory = factory;
            httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IAppointmentRepository, AppointmentRepository>();
                });
            })
        .CreateClient();
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await httpClient.GetAsync("api/appointments");
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WhenCreateWithUserNotFound_ThenReturnNotFound()
        {
            //Arange
            var appoitnmentDto = new AppointmentCreateDto(Guid.NewGuid(), Guid.NewGuid(), DateTime.Today.AddDays(1), "title", "description", "type");

            //Act

            var appointmentResponse = await httpClient.PostAsJsonAsync("api/appointments", appoitnmentDto);
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenCreateValid_ThenReturnCreated()
        {
            //Arange
            var userDto1 = new DefaultUserDto()
            {
                UserId = Guid.NewGuid(),
                Username = "username",
                Password = "password"
            };
            var userResponse = await httpClient.PostAsJsonAsync("api/users", userDto1);
            var userDto2 = new DefaultUserDto()
            {
                UserId = Guid.NewGuid(),
                Username = "username",
                Password = "password"
            };
            var userResponse2 = await httpClient.PostAsJsonAsync("api/users", userDto2);
            var user1 = await userResponse.Content.ReadFromJsonAsync<DefaultUserDto>();
            var user2 = await userResponse2.Content.ReadFromJsonAsync<DefaultUserDto>();
            
            var appoitnmentDto = new AppointmentCreateDto(user1.UserId, user2.UserId, DateTime.Today.AddDays(1), "title", "description", "type");

            //Act

            var appointmentResponse = await httpClient.PostAsJsonAsync("api/appointments", appoitnmentDto);
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }


        [Fact]
        public async Task WhenDeleteNonExistingAppointment_ThenReturnNotFound()
        {
            //Act
            var appointmentResponse = await httpClient.DeleteAsync($"api/appointments/{Guid.NewGuid()}");
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenGetByNonExistingId_ThenReturnNotFound()
        {
            var nonEexistingAppointment = Guid.NewGuid();
            //Act
            var appointmentResponse = await httpClient.GetAsync($"api/appointments/{nonEexistingAppointment}");
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

    }
}
