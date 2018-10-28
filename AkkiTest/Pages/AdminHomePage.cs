using AkkiTest.Tests;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkiTest.Pages
{
    public class AdminHomePage: BasePage

    {
        public AdminHomePage(IWebDriver driver) : base(driver)
        {
            GivenIamOnLoginPage();
            WhenIEnterLoginDetailsAndSubmit("Talsania", "1qa1qa1qa");
        }
       
        [FindsBy(How = How.Id, Using = "linkLogout")]
        private IWebElement LogOutBtn => Driver.FindElement(By.Id("linkLogout"));

        public void goToPage()
        {
            Driver.Navigate().GoToUrl("https://animaltrace-uat.nait.co.nz/AdminHome");
        }

        public void LogOut()
        { 
           LogOutBtn.Click();
        }
    }
}

