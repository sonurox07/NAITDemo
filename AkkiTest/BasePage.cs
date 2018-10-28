using AkkiTest.Tests;
using OpenQA.Selenium;

namespace AkkiTest
{
    public class BasePage 
    {
        public IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; set; }
        public void GivenIamOnLoginPage()
        {
            Driver.Navigate().GoToUrl("https://animaltrace-uat.nait.co.nz/Account/Login.aspx?ReturnUrl=%2f");
        }

        public void WhenIEnterLoginDetailsAndSubmit(string userName, string password)
        {
            LoginPage login = new LoginPage(Driver);
            login.EnterLoginDetailsAndSubmit(userName, password);
            System.Threading.Thread.Sleep(1000);
        }
    }
}
