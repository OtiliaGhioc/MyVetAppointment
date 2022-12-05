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
            //drugs.Should().HaveCount(1);
            CleanDatabases();
        }

        /*[Fact]
        public async void WhenUpdateDrug_ThenShouldReturnNoContent()
        {
            //Arange
            var drugDto = new CreateDrugDto
            {
                Title = "One Drug",
                Price = 20
            };
            var updateDrug = new CreateDrugDto
            {
                Title = "Drug2",
                Price = 200
            };
            var drugResponse = await HttpClient.PostAsJsonAsync(ApiUrl, drugDto);
            var drug = await drugResponse.Content.ReadFromJsonAsync<DrugDto>();

            //Act
           var resultResponse = await HttpClient.PutAsJsonAsync($"{ApiUrl}/{drug.Id}", updateDrug);

            //Assert
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            CleanDatabases();
        }

        [Fact]
        public async void WhenDeleteDrug_ThenShouldReturnNoContent()
        {
            //Arange
            var drugDto = new CreateDrugDto
            {
                Title = "One Drug",
                Price = 20
            };
            var drugResponse = await HttpClient.PostAsJsonAsync(ApiUrl, drugDto);
            var drug = await drugResponse.Content.ReadFromJsonAsync<DrugDto>();

            //Act
            var resultResponse = await HttpClient.DeleteAsync($"{ApiUrl}/{drug.Id}");

            //Assert
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            CleanDatabases();
        }*/
    }
}
