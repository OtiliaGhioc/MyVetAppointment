using System.Net.Http.Json;
using VetAppointment.WebAPI.DTOs;
using Microsoft.AspNetCore.TestHost;
using VetAppointment.Application.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace VetAppointment.Tests.ITs
{
    public class DrugsControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient httpClient;
        private readonly TestingWebAppFactory<Program> factory;
        private const string ApiUrl = "api/drugs";

        public DrugsControllerTests(TestingWebAppFactory<Program> factory)
        {
            this.factory = factory;
            httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    ServiceCollectionServiceExtensions.AddScoped<IDrugRepository, DrugRepository>(services);
                });
            })
        .CreateClient();
        }

        [Fact]
        public async void WhenCreateDrug_ThenShouldReturnOk()
        {
            //Arange
            var drugDto = new CreateDrugDto
            {
                Title = "One Drug",
                Price = 20
            };

            //Act
            var drugResponse = await httpClient.PostAsJsonAsync(ApiUrl, drugDto);
            var getDrugResult = await httpClient.GetAsync(ApiUrl);

            //Assert
            drugResponse.EnsureSuccessStatusCode();
            drugResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getDrugResult.EnsureSuccessStatusCode();
            var drugs = await getDrugResult.Content.ReadFromJsonAsync<List<DrugDto>>();
            drugs.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_WhenCalledAll_ReturnsOk()
        {
            //Act
            var response = await httpClient.GetAsync(ApiUrl);
            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
