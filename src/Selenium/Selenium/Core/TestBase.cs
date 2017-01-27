﻿using System;
using NUnit.Framework;
using Selenium.Core.Browsers;

namespace Selenium.Core
{
    [SetUpFixture]
    public class TestBase
    {
        private readonly Lazy<BrowserFactory> _factory = new Lazy<BrowserFactory>();
        protected IBrowser Driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver = _factory.Value.GetBrowser(Config.Browser);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Driver.Page.Quit();
        }
    }
}