using Microsoft.VisualStudio.TestTools.UnitTesting;
using GiftOfTheGivers.Helpers;
using System.Collections.Generic;

namespace GiftOfTheGivers1.Tests
{
    [TestClass]
    public class DonationHelperTests
    {
        [TestMethod]
        public void FormatTaxCertificateNumber_ValidInput_ReturnsPaddedString()
        {
            // Arrange
            int donationId = 45;
            int year = 2026;
            string expected = "TC-2026-000045"; // Expects 6-digit padding

            // Act
            string actual = DonationHelper.FormatTaxCertificateNumber(donationId, year);

            // Assert
            Assert.AreEqual(expected, actual, "The certificate number did not format with the correct 6-digit padding.");
        }

        [TestMethod]
        public void CalculateDonationTotal_ListOfAmounts_ReturnsCorrectSum()
        {
            // Arrange
            var amounts = new List<decimal> { 150.50m, 200.00m, 100.25m };
            decimal expectedTotal = 450.75m;

            // Act
            decimal actualTotal = DonationHelper.CalculateDonationTotal(amounts);

            // Assert
            Assert.AreEqual(expectedTotal, actualTotal, "The total sum calculated by the helper is incorrect.");
        }

        [TestMethod]
        public void CalculateDonationTotal_EmptyList_ReturnsZero()
        {
            // Arrange
            var amounts = new List<decimal>();
            decimal expectedTotal = 0.00m;

            // Act
            decimal actualTotal = DonationHelper.CalculateDonationTotal(amounts);

            // Assert
            Assert.AreEqual(expectedTotal, actualTotal, "An empty donation list should result in a total of 0.");
        }
    }
}