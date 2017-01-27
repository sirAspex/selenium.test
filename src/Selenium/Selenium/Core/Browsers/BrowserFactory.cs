using System;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;

namespace Selenium.Core.Browsers
{
    public sealed class BrowserFactory :
        AbstractFactory,
        IBrowserWebDriver<FirefoxDriver>,
        IBrowserWebDriver<ChromeDriver>,
        IBrowserWebDriver<EdgeDriver>,
        IBrowserWebDriver<InternetExplorerDriver>,
        IBrowserWebDriver<RemoteWebDriver>
    {

        private readonly string _parentDirName;

        public BrowserFactory()
        {
            _parentDirName = GetParentDirName();
        }


        IBrowser<ChromeDriver> IBrowserWebDriver<ChromeDriver>.Create()
        {
            return new BrowserAdapter<ChromeDriver>(new ChromeDriver(_parentDirName + @"\libs"), BrowserType.Chrome);
        }

        IBrowser<FirefoxDriver> IBrowserWebDriver<FirefoxDriver>.Create()
        {
            var service = FirefoxDriverService.CreateDefaultService(_parentDirName + @"\libs");
            {
                service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            }
           
            return new BrowserAdapter<FirefoxDriver>(new FirefoxDriver(service), BrowserType.Firefox);
        }

        IBrowser<EdgeDriver> IBrowserWebDriver<EdgeDriver>.Create()
        {
            throw new NotImplementedException();

            var service = EdgeDriverService.CreateDefaultService();
            {

            }

            return new BrowserAdapter<EdgeDriver>
            (
                new EdgeDriver(service), BrowserType.Edge
            );
        }

        IBrowser<InternetExplorerDriver> IBrowserWebDriver<InternetExplorerDriver>.Create()
        {
            throw new NotImplementedException();

            var service = InternetExplorerDriverService.CreateDefaultService();
            {
              
            }

            return new BrowserAdapter<InternetExplorerDriver>
            (
                new InternetExplorerDriver(service), BrowserType.Explorer
            );
        }

        IBrowser<RemoteWebDriver> IBrowserWebDriver<RemoteWebDriver>.Create()
        {
            DesiredCapabilities capabilities;
            var gridUrl = Config.GridHubUri;

            switch (Config.Browser)
            {
                case BrowserType.Chrome:
                    capabilities = DesiredCapabilities.Chrome();
                    break;
                case BrowserType.Firefox:
                    capabilities = DesiredCapabilities.Firefox();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Config.RemoteBrowser && Config.UseSauceLabs)
            {
                capabilities.SetCapability(CapabilityType.Version, "50");
                capabilities.SetCapability(CapabilityType.Platform, Config.Platform);
                capabilities.SetCapability("username", Config.SauceLabsUsername);
                capabilities.SetCapability("accessKey", Config.SauceLabsAccessKey);
                gridUrl = Config.SauceLabsHubUri;
            }
            else if (Config.RemoteBrowser && Config.UseBrowserstack)
            {
                capabilities.SetCapability(CapabilityType.Version, "50");
                capabilities.SetCapability(CapabilityType.Platform, Config.Platform);
                capabilities.SetCapability("browserstack.debug", Config.BrowserStackDebug);
                capabilities.SetCapability("browserstack.user", Config.BrowserStackUsername);
                capabilities.SetCapability("browserstack.key", Config.BrowserStackAccessKey);
                gridUrl = Config.BrowserStackHubUrl;
            }

            return
                new BrowserAdapter<RemoteWebDriver>(
                    new RemoteWebDriver(new Uri(gridUrl), capabilities), BrowserType.Remote);
        }

        string GetParentDirName()
        {
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory);
            return fileInfo.Directory?.Parent?.FullName;
        }
    }
}