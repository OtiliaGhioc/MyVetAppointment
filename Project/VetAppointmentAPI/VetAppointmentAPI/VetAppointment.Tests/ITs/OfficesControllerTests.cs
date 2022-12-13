using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.Dtos;

namespace VetAppointment.Tests.ITs
{
    public class OfficesControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient httpClient;
        private readonly TestingWebAppFactory<Program> factory;
        public OfficesControllerTests(TestingWebAppFactory<Program> factory)
        {
            this.factory = factory;
            httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IOfficeRepository, OfficeRepository>();
                });
            })
        .CreateClient();
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await httpClient.GetAsync("api/offices");
            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenCreateOffice_ThenReturnCreated()
        {
            //Arange
            var officeDto = new OfficeDto
            {
                OfficeId = Guid.NewGuid(),
                Address = "Strada Zorilor 13, IS, 123456"
            };

            //Act

            var officeResponse = await httpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await httpClient.GetAsync("api/offices");
            //Assert
            officeResponse.EnsureSuccessStatusCode();
            officeResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            office.EnsureSuccessStatusCode();
            var appointments = await office.Content.ReadFromJsonAsync<List<OfficeDto>>();
            appointments.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenDeleteOffice_ThenReturnNoContent()
        {
            //Arange
            var officeDto = new OfficeDto
            {
                OfficeId = Guid.NewGuid(),
                Address = "Strada Zorilor 13, IS, 123456"
            };

            var officeResponse = await httpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await officeResponse.Content.ReadFromJsonAsync<OfficeDto>();
            //Act
            var officeResult = await httpClient.DeleteAsync($"api/offices/{office.OfficeId}");
            //Assert
            officeResult.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task WhenUpdateOffice_ThenReturnNoContent()
        {
            //Arange
            var officeDto = new OfficeDto
            {
                OfficeId = Guid.NewGuid(),
                Address = "Strada Zorilor 13, IS, 123456"
            };

            

            var officeResponse = await httpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await officeResponse.Content.ReadFromJsonAsync<OfficeDto>();
            office.Address = "Strada Lalelelor 13, IS, 123456";
            //Act
            var officeResult = await httpClient.PutAsJsonAsync($"api/offices", office);
            //Assert
            officeResult.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task WhenGetByIdOffice_ThenReturnOk()
        {
            //Arange
            var officeDto = new OfficeDto
            {
                OfficeId = Guid.NewGuid(),
                Address = "Strada Zorilor 13, IS, 123456"
            };

            var officeResponse = await httpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await officeResponse.Content.ReadFromJsonAsync<OfficeDto>();
            //Act
            var officeResult = await httpClient.GetAsync($"api/offices/{office.OfficeId}");
            //Assert
            officeResult.EnsureSuccessStatusCode();
            officeResult.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
