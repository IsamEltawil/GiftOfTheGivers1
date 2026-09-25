using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGivers1.Pages
{
    public class CertificateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? CertNumber { get; set; }

        public void OnGet()
        {
        }
    }
}
