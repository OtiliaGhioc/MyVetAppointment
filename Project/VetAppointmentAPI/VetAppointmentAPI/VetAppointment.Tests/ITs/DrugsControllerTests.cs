using System.Net.Http.Json;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.Tests.ITs
{
    public class DrugsControllerTests : BaseDrugsIntegrationTests
    {
        private const string ApiUrl = "api/drugs";

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
            var drugResponse = await HttpClient.PostAsJsonAsync(ApiUrl, drugDto);
            var getDrugResult = await HttpClient.GetAsync(ApiUrl);

            //Assert
            drugResponse.EnsureSuccessStatusCode();
            drugResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getDrugResult.EnsureSuccessStatusCode();
            var drugs = await getDrugResult.Content.ReadFromJsonAsync<List<DrugDto>>();
            drugs.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await HttpClient.GetAsync(ApiUrl);
            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
