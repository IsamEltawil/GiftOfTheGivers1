using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace GiftOfTheGivers1.Tests
{
    [TestClass]
    public class SeleniumUiTests
    {
        private IWebDriver _driver;

        // IMPORTANT: Change this to match the URL where your web app runs locally 
        // (Check your browser address bar when running the main project)
        private readonly string _baseUrl = "https://localhost:7192";

        [TestInitialize]
        public void Setup()
        {
            // Opens a fresh Google Chrome window before each test
            _driver = new ChromeDriver();
        }
        [TestMethod]
        public void Scenario1_EmployeeLogin_InvalidCredentials_StaysOnPage()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/Account/Login");

            _driver.FindElement(By.CssSelector("input[type='email']")).SendKeys("employee@giftofthegivers.org");
            _driver.FindElement(By.CssSelector("input[type='password']")).SendKeys("AdminPass123!");

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);

            // Changed IsFalse to IsTrue, and updated the message
            Assert.IsTrue(_driver.Url.Contains("Login"),
                "Test failed: The browser unexpectedly navigated away from the login page.");
        }

        [TestMethod]
        public void Scenario2_AnonymousDonation_SuccessfulSubmit()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/Donate");

            // 1. Fill in the Amount
            _driver.FindElement(By.CssSelector("input[type='number']")).SendKeys("1500");

            // 2. Fill in the required Donor Name to satisfy the browser validation
            _driver.FindElement(By.CssSelector("input[placeholder='John Doe']")).SendKeys("Anonymous Donor");

            // 3. Fill in the Email Address
            _driver.FindElement(By.CssSelector("input[placeholder='donor@example.com']")).SendKeys("anon@example.com");

            // 4. Fill in the Tax / ID Number
            _driver.FindElement(By.CssSelector("input[placeholder='SARS Tax No / ID Number']")).SendKeys("0000000000");

            // 5. Click the "Process Donation" submit button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // 6. Wait for processing and verify success
            Thread.Sleep(2000);
            Assert.IsTrue(_driver.PageSource.Contains("Tax Certificate") || _driver.PageSource.Contains("Thank you"),
                "The success message was not found on the page.");
        }

        [TestCleanup]
        public void Teardown()
        {
            // Closes the browser window after each test finishes
            _driver.Quit();
            _driver.Dispose();
        }
    }
}