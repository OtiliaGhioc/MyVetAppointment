using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.Tests.ITs
{
    public class DrugStocksControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient httpClient;

        private readonly TestingWebAppFactory<Program> factory;
        public DrugStocksControllerTests(TestingWebAppFactory<Program> factory)
        {
            this.factory = factory;
            httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IDrugStockRepository, DrugStockRepository>();
                    services.AddScoped<IDrugRepository, DrugRepository>();
                });
            })
        .CreateClient();
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await httpClient.GetAsync("api/drugstocks");
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

            var dsResponse = await httpClient.PostAsJsonAsync("api/drugstocks", dsDto);
            //Assert
            dsResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenDeleteStockWithUnknownId_ThenReturnNotFound()
        {
            //Arange
            var stockId = Guid.NewGuid();

            //Act
            var dsResult = await httpClient.DeleteAsync($"api/drugstocks/{stockId}");
            //Assert
            dsResult.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenGetByUnknownIdStock_ThenReturnNotFound()
        {
            //Arange
            var stockId = Guid.NewGuid();
            //Act
            var officeResult = await httpClient.GetAsync($"api/durgstocks/{stockId}");
            //Assert
            officeResult.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
