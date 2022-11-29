using System.Net.Http.Json;
using VetAppointment.WebAPI.Dtos;

namespace VetAppointment.Tests.ITs
{
    public class OfficesControllerTests : BaseOfficesIntegrationTests
    {
        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await HttpClient.GetAsync("api/offices");
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WhenCreateOffice_ThenReturnCreated()
        {
            //Arange
            var officeDto = new OfficeDto
            {
                OfficeId = Guid.NewGuid(),
                Address = "aadress"
            };

            //Act

            var officeResponse = await HttpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await HttpClient.GetAsync("api/offices");
            //Assert
            officeResponse.EnsureSuccessStatusCode();
            officeResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            office.EnsureSuccessStatusCode();
            var appointments = await office.Content.ReadFromJsonAsync<List<OfficeDto>>();
            appointments.Should().NotBeNull();
            CleanDatabases();
        }

        [Fact]
        public async Task WhenDeleteOffice_ThenReturnNoContent()
        {
            //Arange
            var officeDto = new OfficeDto
            {
                OfficeId = Guid.NewGuid(),
                Address = "aadress"
            };

            var officeResponse = await HttpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await officeResponse.Content.ReadFromJsonAsync<OfficeDto>();
            //Act
            var officeResult = await HttpClient.DeleteAsync($"api/offices/{office.OfficeId}");
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
                Address = "aadress"
            };

            var officeResponse = await HttpClient.PostAsJsonAsync("api/offices", officeDto);
            var office = await officeResponse.Content.ReadFromJsonAsync<OfficeDto>();
            //Act
            var officeResult = await HttpClient.GetAsync($"api/offices/{office.OfficeId}");
            //Assert
            officeResult.EnsureSuccessStatusCode();
        }
    }
}
