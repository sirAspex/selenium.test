﻿using NUnit.Framework;
using Selenium.Core;
using Selenium.Pages;

namespace Selenium.Tests
{
    [TestFixture]
    [Parallelizable]
    public class GoogleTest : TestBase
    {
        [Test]
        public void TestMethod()
        {
            var page = new HomePage(Driver);
            page.OpenGooglePage();
        }
    }
}