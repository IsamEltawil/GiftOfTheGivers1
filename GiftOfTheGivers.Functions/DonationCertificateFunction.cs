using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GiftOfTheGivers.Functions
{
    public class DonationCertificateFunction
    {
        private readonly ILogger<DonationCertificateFunction> _logger;

        public DonationCertificateFunction(ILogger<DonationCertificateFunction> logger)
        {
            _logger = logger;
        }

        [Function("GenerateDonationCertificate")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("GenerateDonationCertificate function triggered at {Time}", DateTime.UtcNow);

            DonationRequest? donation;

            if (req.Method == "GET")
            {
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                donation = new DonationRequest
                {
                    DonorName = query["donorName"],
                    Amount = decimal.TryParse(query["amount"], out var amt) ? amt : 0,
                    Currency = query["currency"],
                    DonationType = query["donationType"]
                };
            }
            else
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                donation = JsonSerializer.Deserialize<DonationRequest>(body,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // Validation
            if (donation is null
                || string.IsNullOrWhiteSpace(donation.DonorName)
                || string.IsNullOrWhiteSpace(donation.Currency)
                || string.IsNullOrWhiteSpace(donation.DonationType)
                || donation.Amount <= 0)
            {
                _logger.LogWarning("Donation request failed validation: {Body}", donation);
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteAsJsonAsync(new
                {
                    error = "Invalid donation request. donorName, currency, donationType are required and amount must be greater than 0."
                });
                return badResponse;
            }

            // Dummy tax certificate generation
            var certificateNumber = $"GOTG-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";

            var result = new
            {
                certificateNumber,
                donorName = donation.DonorName,
                amount = donation.Amount,
                currency = donation.Currency,
                donationType = donation.DonationType,
                issuedOn = DateTime.UtcNow,
                summary = $"Thank you {donation.DonorName} for your {donation.DonationType} donation " +
                          $"of {donation.Amount} {donation.Currency}. This certificate qualifies for a " +
                          $"Section 18A tax deduction."
            };

            _logger.LogInformation("Certificate {CertNumber} generated for {Donor}", certificateNumber, donation.DonorName);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);
            return response;
        }
    }

    public class DonationRequest
    {
        public string? DonorName { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? DonationType { get; set; }
    }
}