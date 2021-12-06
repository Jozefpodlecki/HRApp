using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V96.Cast;
using OpenQA.Selenium.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeleniumExtras.PageObjects;

namespace HRApp.Web.Tests.AutomationTests.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Name, Using = "email")]
        [CacheLookup]
        private IWebElement Email { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        [CacheLookup]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.Name, Using = "login")]
        [CacheLookup]
        private IWebElement Submit { get; set; }

        public void Login(string email, string password)
        {
            Email.SendKeys(email);
            Password.SendKeys(password);
            Submit.Submit();
        }
    }
}
