using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace UIAutomationTesting.Supports
{
    public static class CurrentWebDriver
    {
        private static string browser;
        private static bool runHeadLess;
        private static ChromeDriverService ChromeDriverService { get; set; }
        private static EdgeDriverService EdgeDriverService { get; set; }
        private static string webDriverPath, binaryLocation;
        public static int TimeOutInSeconds { get; set; }
        private static string BaseUrl { get; set; }
        internal static IWebDriver WebDriver { get; private set; }


        public static void BrowserConfigurations(IConfiguration browserConfig)
        {
            browser = browserConfig.GetSection("selenium").GetValue<string>("browser");
            runHeadLess = browserConfig.GetSection("selenium").GetValue<bool>("runHeadless");
            TimeOutInSeconds = browserConfig.GetSection("selenium").GetValue<int>("timeOutInSeconds");
            BaseUrl = browserConfig.GetValue<string>("baseUrl");
            webDriverPath = browserConfig.GetSection("selenium").GetValue<string>("webdriverLocation");
            binaryLocation = browserConfig.GetSection("selenium").GetValue<string>("binaryLocation");
        }


        public static IWebDriver CreateDriver()
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    {
                        var chromeOptions = new ChromeOptions()
                        {
                            AcceptInsecureCertificates = true
                        };
                        if (runHeadLess)
                        {
                            Console.WriteLine("Running in HeadLess mode");
                            chromeOptions.AddArguments("--headless");
                        }
                        if (webDriverPath.Length > 0)
                        {
                            ChromeDriverService = ChromeDriverService.CreateDefaultService(webDriverPath);

                        }
                        else if (binaryLocation.Length > 0)
                        {
                            Console.WriteLine("Calling Chrome binary...");
                            chromeOptions.BinaryLocation = binaryLocation;
                            ChromeDriverService = ChromeDriverService.CreateDefaultService();
                        }
                        else
                        {
                            Console.WriteLine("calling default chrome-driver Selenium Server");
                            ChromeDriverService = ChromeDriverService.CreateDefaultService();

                        }
                        ChromeDriverService.SuppressInitialDiagnosticInformation = true;

                        WebDriver = new ChromeDriver(ChromeDriverService, chromeOptions);
                        break;

                    }
                case "edge":
                    {
                        var edgeOptions = new EdgeOptions()
                        {
                            AcceptInsecureCertificates = true
                        };
                        if (runHeadLess)
                        {
                            Console.WriteLine("Running in HeadLess mode");
                            edgeOptions.AddArguments("--headless");
                        }
                        if (webDriverPath.Length > 0)
                        {
                            EdgeDriverService = EdgeDriverService.CreateDefaultService(webDriverPath);

                        }
                        else if (binaryLocation.Length > 0)
                        {
                            Console.WriteLine("Calling Chrome binary...");
                            edgeOptions.BinaryLocation = binaryLocation;
                            EdgeDriverService = EdgeDriverService.CreateDefaultService();
                        }
                        else
                        {
                            Console.WriteLine("calling default chrome-driver Selenium Server");
                            EdgeDriverService = EdgeDriverService.CreateDefaultService();

                        }
                        EdgeDriverService.SuppressInitialDiagnosticInformation = true;

                        WebDriver = new EdgeDriver(EdgeDriverService, edgeOptions);
                        break;

                    }

                default:
                    {
                        Console.WriteLine("BrowserName is Invalid. Defaulting to Edge web-driver");
                        EdgeDriverService = EdgeDriverService.CreateDefaultService();
                        WebDriver = new EdgeDriver(EdgeDriverService);
                        break;

                    }


            }
            return WebDriver;

        }




    }
}

