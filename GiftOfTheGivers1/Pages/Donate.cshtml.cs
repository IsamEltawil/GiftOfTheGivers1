using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiftOfTheGivers.Helpers;

namespace GiftOfTheGivers1.Pages
{
    public class DonateModel : PageModel
    {
        [BindProperty]
        public decimal DonationAmount { get; set; }

        public string GeneratedCertificateNumber { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            int dummyDonationId = 1;
            GeneratedCertificateNumber = DonationHelper.FormatTaxCertificateNumber(dummyDonationId, 2026);
        }
    }
}