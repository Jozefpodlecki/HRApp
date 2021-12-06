using FluentAssertions;
using HRApp.Web.Tests.AutomationTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sanakan.Web.Tests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRApp.Web.Tests.AutomationTests
{
#if DEBUG
    [TestClass]
#endif
    public class AppTests
    {
        private static HttpClient _client;
        private static ChromeDriver _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var factory = new TestWebApplicationFactory();
            _client = factory.CreateClient();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("start-maximized");
            _driver = new ChromeDriver(chromeOptions);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _client.Dispose();
            _driver.Quit();
        }

        [TestMethod]
        public async Task Should_Log_In_To_Site()
        {
            _driver.Url = _client.BaseAddress.ToString();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(dr => ((IJavaScriptExecutor)dr).ExecuteScript("return document.readyState").Equals("complete"));

            var page = new LoginPage(_driver);
            page.Login("employee1@test.com", "123");
        }
    }
}
