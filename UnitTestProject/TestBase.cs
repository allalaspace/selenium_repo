//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.PhantomJS;
//using OpenQA.Selenium.Remote;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Concurrent;
//using System.IO;
//using System.Text;
//using System.Threading;

//namespace UnitTestProject
//{


//    [TestClass]
//    public class TestBase
//    {
//        public bool _acceptNextAlert = true;
//        protected IWebDriver _driver;
//        protected WebDriverWait _wait;
//        protected StringBuilder _verificationErrors;
//        protected readonly ConcurrentQueue<IWebDriver> _clones = new ConcurrentQueue<IWebDriver>();
//        protected DesiredCapabilities capabilities = new DesiredCapabilities();


//        protected Func<IWebDriver> Cloner;
//        protected IWebDriver WebDriver { get { return _driver; } }
//        protected WebDriverWait Wait { get { return _wait; } }

//        [TestInitialize]
//        public void SetupTest()
//        {
//            Action setUpAction = Setup;

//            setUpAction.ExecuteOnce("SetupTest");
//        }



//        private void Setup()
//        {
//            ConfigForPhantomJs();
//            //switch (false)
//            //{
//            //    case "true":
//            //        { ConfigForChrome(); break; }
//            //    case "false":
//            //        { ConfigForPhantomJs(); break; }
//            //    default:
//            //        { ConfigForChrome(); break; }
//            //}

//            _driver = Cloner();
//            _wait = new WebDriverWait(_driver, new TimeSpan(0, 2, 0));
//            _verificationErrors = new StringBuilder();
//        }

//        protected IWebDriver CopyDriver()
//        {
//            IWebDriver clone = Cloner();
//            _clones.Enqueue(clone);
//            return clone;
//        }


//        public void ConfigForChromeRemote()
//        {
            
//                var chromedriverPath = Path.Combine(@"F:\chromedriver", "chromedriver.exe");

//                if (!File.Exists(chromedriverPath))
//                {
//                    throw new FileNotFoundException(string.Format(
//                        "chromedriver.exe was not found at {0}, please add it and run the test. \n you can download the latest version from this location {1}",
//                        chromedriverPath, "http://chromedriver.storage.googleapis.com/index.html"));
//                }
//                //var option = new ChromeOptions();
//                var options = new ChromeOptions() { LeaveBrowserRunning = true };
//                options.AddArgument("--start-maximized");
//                //Cloner = () => new ChromeDriver(@"c:\chromedriver");
//                capabilities = DesiredCapabilities.Chrome();
//                capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
//                capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.WinNT));
//                Cloner = () => new RemoteWebDriver(new Uri("http://10.234.1.157:4444/wd/hub"), capabilities);


//        }

           
//            //public void ConfigForPhantomJsRemote()
//            //{
//            //    if (!ConfigHelper.GetFromAppSettings<Boolean>(Constants.TestOnLocalMachine))
//            //    {
//            //        var phantomJsdriver = Path.Combine(AppSettings.PhantomJsPath, "phantomjs.exe");

//            //        if (!File.Exists(phantomJsdriver))
//            //        {
//            //            throw new FileNotFoundException(string.Format(
//            //                "phantomJsdriver.exe was not found at {0}, please add it and run the test. \n you can download the latest version from this location {1}",
//            //                phantomJsdriver, AppSettings.ChromeDriverDownloadPath));
//            //        }
//            //        var option = new PhantomJSOptions();
//            //        //Cloner = () => new PhantomJSDriver(@"c:\phantomJsdriver"); //

//            //        capabilities = DesiredCapabilities.PhantomJS();
//            //        capabilities.SetCapability(CapabilityType.BrowserName, "phantomjs");
//            //        capabilities.SetCapability("phantomjs.binary.path", @"C:\phantomJsdriver\phantomjs.exe");
//            //        capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
//            //        capabilities.SetCapability(CapabilityType.Version, "Any");
//            //        Cloner = () => new EnhancedRemoteWebDriver(new Uri(AppSettings.SeleniumHubUri), capabilities); 

//            //    }
//            // }

//            //Génération Siren
          
//        [TestCleanup]
//        public void CleanupTest()
//        {
//            Action cleanup = Cleanup;

//            cleanup.ExecuteOnce("CleanupTest");
//        }

//        private void Cleanup()
//        {
//            try
//            {
//                _clones.Foreach(driver =>
//                {
//                    try
//                    {
//                        driver.Quit();
//                    }
//                    catch (Exception ex)
//                    {
//                        this.Log().Error(ex);
//                    }

//                });

//                _driver.Quit();
//            }
//            catch (Exception e)
//            {
//                //this.Log().Error(e);
//            }

//            Assert.AreEqual("", _verificationErrors.ToString());
//        }

//        protected bool IsElementPresent(By by)
//        {
//            try
//            {
//                _driver.FindElement(by);
//                return true;
//            }
//            catch (NoSuchElementException)
//            {
//                return false;
//            }
//        }

//        protected bool IsAlertPresent()
//        {
//            try
//            {
//                _driver.SwitchTo().Alert();
//                return true;
//            }
//            catch (NoAlertPresentException)
//            {
//                return false;
//            }
//        }

//        protected string CloseAlertAndGetItsText()
//        {
//            try
//            {
//                Thread.Sleep(500);
//                var alert = _driver.SwitchTo().Alert();
//                string alertText = alert.Text;

//                if (_acceptNextAlert)
//                {
//                    alert.Accept();
//                }
//                else
//                {
//                    alert.Dismiss();
//                }
//                return alertText;
//            }
//            finally
//            {
//                _acceptNextAlert = true;
//            }
//        }

//        public IAlert GetSeleniumAlert()
//        {
//            //Don't handle Alerts using .SwitchTo() for PhantomJS
//            if (_driver is PhantomJSDriver)
//            {
//                var js = _driver as IJavaScriptExecutor;
//                var result = js.ExecuteScript("window.confirm = function(){return true;}") as string;
//                ((PhantomJSDriver)_driver).ExecuteScript("var page = this;" + "page.onConfirm = function(msg) {" +
//                                                               "console.log('CONFIRM: ' + msg);return true;" +
//                                                                "};");
//                return null;
//            }

//            try
//            {
//                return _driver.SwitchTo().Alert();
//            }
//            catch (NoAlertPresentException)
//            {
//                return null;
//            }
//        }

//        public void CloseJavascriptAlert(string AlertText)
//        {
//            if (_driver is PhantomJSDriver)
//            {
//                PhantomJSDriver phantom = (PhantomJSDriver)_driver;
//                phantom.ExecuteScript("window.alert = function(){return true;}");
//            }
//            else
//            {
//                Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), AlertText));
//            }
//        }

//        protected bool IsAlertPresent(IWebDriver drv)
//        {
//            try
//            {
//                drv.SwitchTo().Alert();
//                return true;
//            }
//            catch (NoAlertPresentException)
//            {
//                return false;
//            }
//        }

//        protected string CloseAlertAndGetItsText(IWebDriver drv)
//        {
//            return CloseAlertAndGetItsText(drv, _acceptNextAlert);
//        }

//        protected virtual void WaitUntilIsVisibleAndExist(By Locator)
//        {
//            Wait.Until(ExpectedConditions.ElementExists(Locator));
//            Wait.Until(ExpectedConditions.ElementIsVisible(Locator));
//        }

//        protected virtual void WaitUntilNoOverlay(IWebDriver drv)
//        {
//            // Wait.Until(driver => !driver.FindElement(By.ClassName("blockUI")).Displayed);
//            if (drv.FindElement(By.CssSelector("body > div.blockUI.blockOverlay")).Displayed)
//            {
//                Wait.Until(ExpectedConditions.ElementExists(By.CssSelector("body > div.blockUI.blockOverlay")));
//            }

//        }

//        protected virtual void WaitUntilOverlayDisappears()
//        {
//            //var overlayElement = _driver.FindElement(By.CssSelector("body > div.blockUI.blockOverlay"));
//            //do
//            //{
//            //    overlayElement = _driver.FindElement(By.CssSelector("body > div.blockUI.blockOverlay"));
//            //    Thread.Sleep(500);
//            //} while (overlayElement == null);
//            try
//            {
//                var overlayElement = _driver.FindElement(By.CssSelector("body > div.blockUI.blockOverlay"));
//                do
//                {
//                    overlayElement = _driver.FindElement(By.CssSelector("body > div.blockUI.blockOverlay"));
//                    Thread.Sleep(500);
//                } while (overlayElement == null);
//            }
//            catch
//            {

//                this.Log().Debug("blockoverlay Exception");
//                Thread.Sleep(500);

//            }

//        }

//        protected string CloseAlertAndGetItsText(IWebDriver drv, bool acceptNextAlert)
//        {
//            try
//            {
//                IAlert alert = drv.SwitchTo().Alert();
//                string alertText = alert.Text;

//                if (acceptNextAlert)
//                {
//                    alert.Accept();
//                }
//                else
//                {
//                    alert.Dismiss();
//                }
//                return alertText;
//            }
//            finally
//            {
//                acceptNextAlert = true;
//            }
//        }

//        protected virtual void JasonLogin(string login = "SELENIUMSEVIA09")
//        {
//            _driver.Navigate().GoToUrl(AppSettings.JasonBaseUrl + "/Profils/Utilisateur/LogOn?SsoChecked=true");
//            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Login")));
//            _driver.FindElement(By.Id("Login")).Clear();
//            _driver.FindElement(By.Id("Login")).SendKeys(login);
//            _driver.FindElement(By.Id("Password")).Clear();
//            _driver.FindElement(By.Id("Password")).SendKeys("P@ssword1");
//            _driver.FindElement(By.CssSelector("input.j-btn.j-primary")).Click();
//            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("start-button")));
//        }
//        protected virtual void JasonLoginFacture(string login = "Sarp4027V")
//        {
//            _driver.Navigate().GoToUrl(AppSettings.JasonBaseUrl + "/Profils/Utilisateur/LogOn?SsoChecked=true");
//            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Login")));
//            _driver.FindElement(By.Id("Login")).Clear();
//            _driver.FindElement(By.Id("Login")).SendKeys(login);
//            _driver.FindElement(By.Id("Password")).Clear();
//            _driver.FindElement(By.Id("Password")).SendKeys("Test!sel");
//            _driver.FindElement(By.CssSelector("input.j-btn.j-primary")).Click();
//            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("start-button")));
//        }
//        public void WaitBlockOver(By by, Action<IWebElement> action)
//        {
//            try
//            {
//                action(_driver.FindElement(by));
//            }
//            catch (Exception e)
//            {
//                this.Log().Debug("WaitBlockOver" + e.ToString());
//                Thread.Sleep(1000);
//                action(_driver.FindElement(by));
//            }


//        }

//        #region Login
//        protected virtual void JasonLogin()
//        {
//            //var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
//            _driver.Navigate().GoToUrl(AppSettings.JasonBaseUrl + "/Profils/Utilisateur/logon?ssoChecked=True");

//            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Login")));
//            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
//            try
//            {
//                _driver.FindElement(By.Id("Login")).Click();
//                _driver.FindElement(By.Id("Login")).Clear();
//                _driver.FindElement(By.Id("Login")).SendKeys(AppSettings.Login);
//            }
//            catch
//            {
//                _driver.FindElement(By.CssSelector("#Login")).Click();
//                _driver.FindElement(By.CssSelector("#Login")).Clear();
//                _driver.FindElement(By.CssSelector("#Login")).SendKeys(AppSettings.Login);
//            }
//            // finally(Exception)
//            // {
//            //   Assert.Fail("Login");    
//            //}

//            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));
//            _driver.FindElement(By.Id("Password")).Click();
//            _driver.FindElement(By.Id("Password")).Clear();
//            _driver.FindElement(By.Id("Password")).SendKeys(AppSettings.Password);
//            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input.j-btn.j-primary")));
//            _driver.FindElement(By.CssSelector("input.j-btn.j-primary")).Click();
//        }
//        protected virtual void JasonLogOut()
//        {
//            WebDriver.Navigate().GoToUrl(AppSettings.JasonBaseUrl + "/Profils/Utilisateur/LogOut");
//        }
//        #endregion

//        //dont work
//        public IWebElement FindElementWait(IWebDriver driver, By by, int timeoutInSeconds)
//        {
//            if (timeoutInSeconds > 0)
//            {
//                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
//                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeoutInSeconds));
//                wait.Until(ExpectedConditions.ElementExists(by));
//                wait.Until(ExpectedConditions.ElementIsVisible(by));
//                return wait.Until(drv => drv.FindElement(by));
//            }
//            return driver.FindElement(by);
//        }
//        public bool ElementIsPresent(IWebDriver driver, By by)
//        {
//            try
//            {
//                return driver.FindElement(by).Displayed;
//            }
//            catch (NoSuchElementException)
//            {
//                return false;
//            }
//        }
//        public void verifyChecked(By by, string message)
//        {
//            this.Log().Debug("method check");
//            if (!_driver.FindElement(by).Selected)
//            {
//                WaitBlockOver(by, e => e.Click());
//                this.Log().Debug("ligne checked");

//            }
//            else
//            {
//                this.Log().Debug(message);
//            }

//        }
//        public void MoveTo(By by)
//        {
//            var element = _driver.FindElement(by);
//            Actions actions = new Actions(_driver);
//            Thread.Sleep(500);
//            actions.MoveToElement(element).Perform();

//        }


//        public void ConfigForChrome()
//        {
            
//                var chromedriverPath = Path.Combine(AppSettings.ChromeDriverPath, "chromedriver.exe");

//                if (!File.Exists(chromedriverPath))
//                {
//                    throw new FileNotFoundException(string.Format(
//                        "chromedriver.exe was not found at {0}, please add it and run the test. \n you can download the latest version from this location {1}",
//                        chromedriverPath, AppSettings.ChromeDriverDownloadPath));
//                }
//                var option = new ChromeOptions();

//                option.AddArguments("test-type");
//                option.AddArguments("start-maximized");

//                Cloner = () => new ChromeDriver(AppSettings.ChromeDriverPath);//new EnhancedRemoteWebDriver(new Uri(AppSettings.SeleniumHubUri), DesiredCapabilities.Chrome());

            

//        }

//        public void ConfigForPhantomJs()
//        {
//            if (!ConfigHelper.GetFromAppSettings<Boolean>(Constants.TestOnLocalMachine))
//            {
//                var phantomJsdriver = Path.Combine(AppSettings.PhantomJsPath, "phantomjs.exe");

//                if (!File.Exists(phantomJsdriver))
//                {
//                    throw new FileNotFoundException(string.Format(
//                        "phantomJsdriver.exe was not found at {0}, please add it and run the test. \n you can download the latest version from this location {1}",
//                        phantomJsdriver, AppSettings.ChromeDriverDownloadPath));
//                }
//                var option = new PhantomJSOptions();
//                Cloner = () => new PhantomJSDriver(AppSettings.PhantomJsPath); //new EnhancedRemoteWebDriver(new Uri(AppSettings.SeleniumHubUri), DesiredCapabilities.Chrome()); 

//            }
//        }


//    }

//    public static class DriverExtensions
//    {


//        public static IWebDriver NoWaitFor(this IWebDriver driver, Action<IWebDriver> doAction)
//        {
//            TurnOffWait(driver);
//            doAction(driver);
//            TurnOnWait(driver);
//            return driver;
//        }

//        private static void TurnOnWait(IWebDriver driver)
//        {
//            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
//        }

//        private static void TurnOffWait(IWebDriver driver)
//        {
//            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
//        }


//    }

//    public class paramTest
//    {
//        public string UrlChange { get; set; }
//        public string IdTiers { get; set; }
//    }

//    public static class ActionExtensions
//    {
//        private static readonly ConcurrentDictionary<string, Lazy<Action>> Actions = new ConcurrentDictionary<string, Lazy<Action>>();

//        public static void ExecuteOnce(this Action routine, string uniqueName)
//        {
//            var val = Actions.GetOrAdd(uniqueName, name => new Lazy<Action>(() =>
//            {
//                routine();
//                return routine;
//            })).Value;
//        }
//    }


//}

