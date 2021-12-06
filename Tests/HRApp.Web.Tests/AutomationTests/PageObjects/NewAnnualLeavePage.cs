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
    public class NewAnnualLeavePage
    {
        private readonly IWebDriver _driver;

        public NewAnnualLeavePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
