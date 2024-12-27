using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using System.IO;
using TechTalk.SpecFlow.Infrastructure;

namespace UIAutomationTesting.Supports
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private static IWebDriver driver;
        private static string screenshotPath;

        private static IConfigurationRoot Config { get; set; }

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun(Order = 0)]
        public static void StartUp()
        {
            var environment = Environment.GetEnvironmentVariable("Environment")?.ToLower();
            Console.WriteLine($"Environment value : {environment?.ToUpper()}");
            Config = new ConfigurationBuilder()
                .AddJsonFile(environment != null ? $"Config/appsettings.{environment}.json" : "Config/appsettings.local.json",
                optional: false, reloadOnChange: true)
                .Build();
        }

        [BeforeFeature]
        public static void BeforeFeatureUI()
        {
            // Configure the browser settings
            CurrentWebDriver.BrowserConfigurations(Config);

            // Create the WebDriver instance
            driver = CurrentWebDriver.CreateDriver();
        }

        [AfterStep]
        public static void AfterStep(ISpecFlowOutputHelper outputHelper)
        {

            try
            {
                if (driver == null)
                {
                    Console.WriteLine("WebDriver is not initialized.");
                    return;
                }

                // Take a screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                // Save the screenshot to a file
                string screenshotDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotDirectory);
                screenshotPath = Path.Combine(screenshotDirectory, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                screenshot.SaveAsFile(screenshotPath);

                Console.WriteLine("Screenshot saved at: " + screenshotPath);
                outputHelper.AddAttachment(screenshotPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error taking screenshot: " + ex.Message);
            }
        }

        [AfterFeature]
        public static void DisposeWebDriver()
        {
            driver.Quit();
        }
    }
}