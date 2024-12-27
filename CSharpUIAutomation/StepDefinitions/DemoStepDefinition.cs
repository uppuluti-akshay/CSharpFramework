using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using UIAutomationTesting.Supports;

namespace UIAutomationTesting.StepDefinitions
{
    [Binding]
    public class GoogleHomePageSteps
    {
        private readonly IWebDriver driver;

        public GoogleHomePageSteps()
        {
            driver = CurrentWebDriver.WebDriver;
        }

        [Given(@"I navigate to ""(.*)""")]
        public void GivenINavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);

            // Wait for the page to load completely
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            driver.Manage().Window.Maximize();
        }

        [When(@"the homepage is loaded")]
        public void WhenTheHomepageIsLoaded()
        {
            // Wait for the title to be present
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Length > 0);
        }

        [Then(@"the title should be ""(.*)""")]
        public void ThenTheTitleShouldBe(string expectedTitle)
        {
            Assert.AreEqual(expectedTitle, driver.Title);
        }


        [When(@"I enter ""(.*)"" into the search box")]
        public void WhenIEnterIntoTheSearchBox(string searchTerm)
        {
            var searchBox = driver.FindElement(By.XPath("//textarea[@class='gLFyf']"));
            searchBox.SendKeys(searchTerm);
            searchBox.Submit();
        }

        [When(@"I click the search button")]
        public void WhenIClickTheSearchButton()
        {
            Thread.Sleep(10000);
        }

        [Then(@"the search results page is loaded")]
        public void ThenTheSearchResultsPageIsLoaded()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        [Then(@"I click the first search result link")]
        public void ThenIClickTheFirstSearchResultLink()
        {
            var firstResult = driver.FindElement(By.CssSelector("h3"));
            firstResult.Click();
        }
    }
}