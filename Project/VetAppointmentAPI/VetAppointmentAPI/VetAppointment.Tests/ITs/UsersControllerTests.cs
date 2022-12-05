namespace VetAppointment.Tests.ITs
{
    public class UsersControllerTests : BaseUsersIntegrationTests
    {
        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            //Act
            var response = await HttpClient.GetAsync("api/users");
            //Assert
            response.EnsureSuccessStatusCode();
        }
/*
        [Fact]
        public async Task WhenCreateWithUserNotFound_ThenReturnNotFound()
        {
            var userDto = new UserDto
            {
                UserId = Guid.NewGuid(),
                Username = "username",
                Password = "pass"
            };

            //Act

            var appointmentResponse = await HttpClient.PostAsJsonAsync("api/users", userDto);
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task WhenDeleteNonExistingAppointment_ThenReturnNotFound()
        {
            //Act
            var appointmentResponse = await HttpClient.DeleteAsync($"api/users/{Guid.NewGuid()}");
            //Assert
            appointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }*/
    }
}
