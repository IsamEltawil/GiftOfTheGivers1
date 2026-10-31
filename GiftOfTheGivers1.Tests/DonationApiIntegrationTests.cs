using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GiftOfTheGivers1.Tests
{
    [TestClass]
    public class DonationApiIntegrationTests
    {
        [TestMethod]
        public async Task GenerateDonationCertificate_ValidPost_ReturnsSuccess()
        {
            // 1. ARRANGE: Set up the HTTP Client and URL
            using var client = new HttpClient();
            var functionUrl = "http://localhost:7045/api/GenerateDonationCertificate";

            // Match the exact properties expected by the DonationRequest class
            var formData = new
            {
                DonorName = "Isam Eltawil",
                Amount = 1500.00m,
                Currency = "ZAR",
                DonationType = "Monetary"
            };

            var json = JsonSerializer.Serialize(formData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 2. ACT: Send the POST request to the Function
            var response = await client.PostAsync(functionUrl, content);

            // 3. ASSERT: Verify the Function returned an HTTP 200 OK success code
            Assert.IsTrue(response.IsSuccessStatusCode, $"Test failed. The function returned: {response.StatusCode}");

            // 4. ASSERT (Optional but recommended): Verify the certificate was actually generated in the response
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseBody.Contains("certificateNumber"), "Response did not contain a certificate number.");
        }

        [TestMethod]
        public async Task GenerateDonationCertificate_LoadTest_5ConcurrentUsers()
        {
            // 1. ARRANGE: Set up the client and payload
            using var client = new HttpClient();
            var functionUrl = "http://localhost:7045/api/GenerateDonationCertificate";

            var formData = new
            {
                DonorName = "Concurrent Load Test User",
                Amount = 100.00m,
                Currency = "ZAR",
                DonationType = "Monetary"
            };

            var json = JsonSerializer.Serialize(formData);
            var tasks = new List<Task<HttpResponseMessage>>();

            // 2. ACT: Loop 5 times and fire the requests simultaneously
            for (int i = 0; i < 5; i++)
            {
                // We must create fresh HTTP content for every single request in the loop
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                tasks.Add(client.PostAsync(functionUrl, content));
            }

            // Wait for all 5 concurrent "users" to finish their requests
            var responses = await Task.WhenAll(tasks);

            // 3. ASSERT: Verify that the Azure Function successfully handled the load without crashing
            foreach (var response in responses)
            {
                Assert.IsTrue(response.IsSuccessStatusCode,
                    $"A request failed under load. Status Code: {response.StatusCode}");
            }
        }
    }
}