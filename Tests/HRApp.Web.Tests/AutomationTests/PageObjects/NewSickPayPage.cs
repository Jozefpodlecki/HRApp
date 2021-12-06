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
    public class NewSickPayPage
    {

        private readonly IWebDriver _driver;

        public NewSickPayPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
