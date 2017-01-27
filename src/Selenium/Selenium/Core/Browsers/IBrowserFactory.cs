using OpenQA.Selenium;

namespace Selenium.Core.Browsers
{
    public interface IBrowserFactory
    {
        IBrowser Create<T>() where T : IWebDriver;
    }
}