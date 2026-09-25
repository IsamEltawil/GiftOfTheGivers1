namespace GiftOfTheGivers.Helpers
{
    public static class DonationHelper
    {
        // Formats a tax certificate number like TC-2026-000123
        public static string FormatTaxCertificateNumber(int donationId, int year)
        {
            return $"TC-{year}-{donationId.ToString("D6")}";
        }

        // Calculates the total value of a list of donation amounts
        public static decimal CalculateDonationTotal(List<decimal> donationAmounts)
        {
            decimal total = 0;
            foreach (var amount in donationAmounts)
            {
                total += amount;
            }
            return total;
        }
    }
}