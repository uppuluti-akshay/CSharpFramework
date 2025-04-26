using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowCSharpFramework.Support
{
    public class WebDriverUtility
    {
         private IWebDriver? driver;
        public IWebDriver WebDriverInitilization()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("www.google.com");


            return driver;

        }
        
        

    }
}
