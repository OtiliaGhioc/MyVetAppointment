using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Tests.ITs
{
    public class AppointmentsControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public AppointmentsControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            var response = await _client.GetAsync("/Appointments");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsFail()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Appointments");
            var formModel = new Dictionary<string, string>
            {
                { "AppointerId", "3fa85f64-5717-4562-b3fc-2c963f66afa6" },
                { "AppointeeId", "3fa85f64-5717-4562-b3fc-2c963f66afa6" },
                { "Description", "TestDesc" },
                { "Type", "Checkup" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsSuccess()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Users");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "test" },
                { "Password", "pass"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            formModel = new Dictionary<string, string>
            {
                { "Name", "test2" },
                { "Password", "pass2"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var appointerId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            var appointeeId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

            postRequest = new HttpRequestMessage(HttpMethod.Post, "/Appointments");
            formModel = new Dictionary<string, string>
            {
                { "AppointerId", appointerId },
                { "AppointeeId", appointeeId },
                { "Description", "TestDesc" },
                { "Type", "Checkup" },
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);

            response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
        }
    }
}
