using OpenQA.Selenium;

namespace Selenium.Core.Browsers
{
    public interface IBrowserWebDriver<out T> where T : IWebDriver
    {
        IBrowser<T> Create();
    }
}