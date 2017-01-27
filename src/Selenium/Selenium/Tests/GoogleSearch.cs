using NUnit.Framework;
using Selenium.Core;
using Selenium.Pages;

namespace Selenium.Tests
{
    [TestFixture]
    [Parallelizable]
    public class GoogleSearch : TestBase
    {
        [Test]
        public void TestMethod()
        {
            var page = new HomePage(Driver);
            page.Register().CreateAccount();
        }
    }
}