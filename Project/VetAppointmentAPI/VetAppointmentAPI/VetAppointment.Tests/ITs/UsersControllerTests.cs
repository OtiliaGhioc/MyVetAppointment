using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.Dtos.UserDto;
using VetAppointment.WebAPI.DTOs.AuthDtos;

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
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenUpdate_ThenReturnCreated()
        {
            var userDto = new UserAuthDto()
            {
                Username = "username",
                Password = "password",
                IsMedic = false
            };


            var userResponse = await httpClient.PostAsJsonAsync("api/Auth/register", userDto);
            var authResponse = await userResponse.Content.ReadFromJsonAsync<AuthResponseDto>();

            if (authResponse == null)
                throw new Exception();

            //Act
            var updateUser = await httpClient.PutAsJsonAsync($"api/users/{authResponse.UserId}", new DefaultUserDto
            {
                Username = "username2"
            });

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
