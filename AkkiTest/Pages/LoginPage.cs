using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AkkiTest.Tests
{
    public class LoginPage : BasePage
    {

        public LoginPage(IWebDriver driver) : base(driver)
        {}

        [FindsBy(How = How.Id, Using = "txtUserName")]
        public IWebElement txtUserName => Driver.FindElement(By.Id("txtUserName"));
        
        [FindsBy(How = How.Id, Using = "txtPassword")]
        public IWebElement txtPassword => Driver.FindElement(By.Id("txtPassword"));
 
        [FindsBy(How = How.Id, Using = "btnSubmit")]
        public IWebElement btnSubmit => Driver.FindElement(By.Id("btnSubmit"));
        
        public void WhenIClickLoginButton()
        {
            Driver.Navigate().GoToUrl("https://animaltrace-uat.nait.co.nz/Account/Login.aspx?ReturnUrl=%2f");
        }

        public void EnterLoginDetailsAndSubmit(string userName, string password)
        {
            System.Threading.Thread.Sleep(4000);
            TypeUserName(userName);
            TypePassword(password);
            ClickLogOnBtn();
        }

        private void TypeUserName(string userName)
        {
            System.Threading.Thread.Sleep(1000);
            txtUserName.Click();
            txtUserName.Clear();
            txtUserName.SendKeys(userName);
        }

        private void TypePassword(string password)
        {
            txtPassword.Click();
            txtPassword.Clear();
            txtPassword.SendKeys(password);
        }

        private void ClickLogOnBtn()
        {
            btnSubmit.Click();
        }
    }
}
