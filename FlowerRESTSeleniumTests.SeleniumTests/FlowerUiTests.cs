using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FlowerREST.SeleniumTests
{
    [TestClass]
    public class FlowerUiTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void CreateFlowerInput_ShouldExist()
        {
            driver.Navigate().GoToUrl("http://127.0.0.1:5500");

            IWebElement input =
                driver.FindElement(
                    By.CssSelector("input[placeholder='Skriv farve']")
                );

            Assert.IsNotNull(input);
        }

        [TestMethod]
        public void Frontend_ShouldOpen()
        {
            driver.Navigate().GoToUrl("http://127.0.0.1:5500");

            Assert.IsTrue(
                driver.PageSource.Contains("Mine blomster")
            );
        }

        [TestMethod]
        public void CreateFlower_ShouldShowFlowerInTable()
        {
            // Arrange
            driver.Navigate().GoToUrl(
                "http://127.0.0.1:5500"
            );

            // Finder inputfeltet med placeholder="Skriv farve"
            IWebElement input =
                driver.FindElement(
                    By.CssSelector("input[placeholder='Skriv farve']")
                );

            // Finder knappen med teksten "Opret blomst"
            IWebElement createButton =
                driver.FindElement(
                    By.XPath("//button[contains(text(), 'Opret blomst')]")
                );

            // Act
            input.SendKeys("Purple");

            createButton.Click();

            Thread.Sleep(2000);

            // Assert
            Assert.IsTrue(
                driver.PageSource.Contains("Purple")
            );
        }
    }
}