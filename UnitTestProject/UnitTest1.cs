using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        protected DesiredCapabilities capabilities = new DesiredCapabilities();
        [TestMethod]
        public void TestMethod()
        {

            //var uri = "http://localhost:4444/wd/hub";  
            //var capabilities = new ChromeOptions().ToCapabilities();  
            //var commandTimeout = TimeSpan.FromMinutes(5); 
            //var driver = new RemoteWebDriver(new Uri(uri), capabilities, commandTimeout);

            var options = new ChromeOptions() { LeaveBrowserRunning = true };
            options.AddArgument("--start-maximized");
            capabilities = DesiredCapabilities.Chrome();
            capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.WinNT));
            var driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);


            driver.Navigate().GoToUrl("https://www.amazon.com/");
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys("");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("P@ssword1");
            driver.FindElement(By.CssSelector("input.j-btn.j-primary")).Click();

        }
    }
}
