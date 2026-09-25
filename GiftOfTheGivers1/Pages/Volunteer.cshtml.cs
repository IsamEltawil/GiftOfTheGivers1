using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGivers1.Pages
{
    public class VolunteerModel : PageModel
    {
        [BindProperty]
        public string FullName { get; set; } = string.Empty;
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string ContactNumber { get; set; } = string.Empty;
        [BindProperty]
        public string Location { get; set; } = string.Empty;
        [BindProperty]
        public string SkillSet { get; set; } = string.Empty;
        [BindProperty]
        public string Availability { get; set; } = string.Empty;

        public bool Submitted { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(FullName)
                || string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(ContactNumber)
                || string.IsNullOrWhiteSpace(Location))
            {
                ModelState.AddModelError(string.Empty, "Please fill in your name, email, contact number, and location.");
                return Page();
            }

            Submitted = true;
            return Page();
        }
    }
}
