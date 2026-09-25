using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGivers1.Pages
{
    public class CertificateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? CertNumber { get; set; }

        public string DonorName { get; private set; } = "Valued Donor";
        public string Amount { get; private set; } = "-";
        public string Currency { get; private set; } = "";
        public string DonationType { get; private set; } = "One-Time";
        public string IssuedOn { get; private set; } = DateTime.UtcNow.ToString("d MMMM yyyy");
        public string Summary { get; private set; } = "";

        public void OnGet()
        {
            DonorName = TempData["DonorName"] as string ?? DonorName;
            Currency = TempData["Currency"] as string ?? Currency;
            DonationType = TempData["DonationType"] as string ?? DonationType;
            IssuedOn = TempData["IssuedOn"] as string ?? IssuedOn;
            Summary = TempData["Summary"] as string ?? Summary;

            if (decimal.TryParse(TempData["Amount"] as string, out var amount))
            {
                Amount = amount.ToString("N2");
            }
        }
    }
}
