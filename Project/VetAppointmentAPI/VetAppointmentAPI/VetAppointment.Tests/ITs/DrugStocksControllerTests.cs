using System.Net.Http.Json;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.Tests.ITs
{
    public class DrugStocksControllerTests : BaseDrugStocksIT
    {
        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await HttpClient.GetAsync("api/drugstocks");
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WhenCreateDrugStockWithUnknownDrugId_ThenReturnNotFound()
        {
            //Arange
            var dsDto = new CreateDrugStockDto
            {
                Quantity= 1,
                TypeId= Guid.NewGuid()
            };

            //Act

            var dsResponse = await HttpClient.PostAsJsonAsync("api/drugstocks", dsDto);
            //Assert
            dsResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenDeleteStockWithUnknownId_ThenReturnNotFound()
        {
            //Arange
            var stockId = Guid.NewGuid();

            //Act
            var dsResult = await HttpClient.DeleteAsync($"api/drugstocks/{stockId}");
            //Assert
            dsResult.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenGetByUnknownIdStock_ThenReturnNotFound()
        {
            //Arange
            var stockId = Guid.NewGuid();
            //Act
            var officeResult = await HttpClient.GetAsync($"api/durgstocks/{stockId}");
            //Assert
            officeResult.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
