using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Selenium.Core
{
    public interface IPage : IWebDriver
    {
        new string Title { get; }
        new string PageSource { get; }
        new string CurrentWindowHandle { get; }
        new ReadOnlyCollection<string> WindowHandles { get; }
        void GoToUrl(string url);
        new IWebElement FindElement(By by);
        new IEnumerable<IWebElement> FindElements(By selector);
        void NavigateBack();
        void Refresh();
        new void Close();
        new void Quit();
        new IOptions Manage();
        new INavigation Navigate();
        new ITargetLocator SwitchTo();
    }
}