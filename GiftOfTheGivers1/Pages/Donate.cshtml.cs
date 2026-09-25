using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace GiftOfTheGivers1.Pages
{
    public class DonateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DonateModel>
    _logger;

        public DonateModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DonateModel>
            logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [BindProperty]
        public string DonorName { get; set; } = string.Empty;
        [BindProperty]
        public decimal Amount { get; set; }
        [BindProperty]
        public string Currency { get; set; } = "ZAR";
        [BindProperty]
        public string DonationType { get; set; } = "One-Time";

        public void OnGet()
        {
        }

        public async Task<IActionResult>
            OnPostAsync()
        {
            var functionUrl = _configuration["DonationFunction:Url"];
            var functionKey = _configuration["DonationFunction:Key"];

            var client = _httpClientFactory.CreateClient();
            var payload = JsonSerializer.Serialize(new
            {
                donorName = DonorName,
                amount = Amount,
                currency = Currency,
                donationType = DonationType
            });

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var requestUrl = $"{functionUrl}?code={functionKey}";

            var response = await client.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Donation function returned {StatusCode}", response.StatusCode);
                ModelState.AddModelError(string.Empty, "We couldn't process your donation right now. Please try again.");
                return Page();
            }

            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>
                (resultJson);
            var certificateNumber = result.GetProperty("certificateNumber").GetString();

            TempData["DonorName"] = result.GetProperty("donorName").GetString();
            TempData["Amount"] = result.GetProperty("amount").GetDecimal().ToString();
            TempData["Currency"] = result.GetProperty("currency").GetString();
            TempData["DonationType"] = result.GetProperty("donationType").GetString();
            TempData["IssuedOn"] = result.GetProperty("issuedOn").GetDateTime().ToString("d MMMM yyyy");
            TempData["Summary"] = result.GetProperty("summary").GetString();

            return RedirectToPage("/Certificate", new { certNumber = certificateNumber });
        }
    }
}
