using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.Dtos.UserDto;

namespace VetAppointment.Tests.ITs
{
    public class UsersControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient httpClient;
        private readonly TestingWebAppFactory<Program> factory;
        public UsersControllerTests(TestingWebAppFactory<Program> factory)
        {
            this.factory = factory;
            httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    ServiceCollectionServiceExtensions.AddScoped<IUserRepository, UserRepository>(services);
                });
            })
        .CreateClient();
        }
        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await httpClient.GetAsync("api/users");
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WhenCreate_ThenReturnCreated()
        {
            var userDto = new DefaultUserDto()
            {
                UserId = Guid.NewGuid(),
                Username = "username",
                Password = "password"
            };

            //Act

            var userResponse = await httpClient.PostAsJsonAsync("api/users", userDto);
            //Assert
            userResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task WhenUpdate_ThenReturnCreated()
        {
            var userDto = new DefaultUserDto()
            {
                UserId = Guid.NewGuid(),
                Username = "username",
                Password = "password"
            };


            var userResponse = await httpClient.PostAsJsonAsync("api/users", userDto);
            var user = await userResponse.Content.ReadFromJsonAsync<DefaultUserDto>();

            //Act
            var updateUser = await httpClient.PutAsJsonAsync("api/users", user);

            //Assert
            updateUser.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task WhenDeleteNonExistingUser_ThenReturnNotFound()
        {
            //Act
            var userResponse = await httpClient.DeleteAsync($"api/users/{Guid.NewGuid()}");
            //Assert
            userResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
