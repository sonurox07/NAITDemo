using AkkiTest.Pages;
using AkkiTest.Tests;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace AkkiTest
{
    //[TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
   // [TestFixture(typeof(InternetExplorerDriver))]
    //[Parallelizable]

    public class Test<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        [Test]
        public void TestLogin()
        {
            //Initialize Login page
            LoginPage loginPage = new LoginPage(driver);
            loginPage.GivenIamOnLoginPage();

            //System.Threading.Thread.Sleep(1000);
            //When 
            loginPage.EnterLoginDetailsAndSubmit("Talsania", "1qa1qa1qa");
            //Then I am on AdminHomePage
            Assert.AreEqual("NAIT", driver.Title);
            //System.Threading.Thread.Sleep(1000);

            //Test report
            test = extent.CreateTest("TestLogin");

            //test.Log(Status.Pass, "TestPassed");
        }


        [Test]
        public void LoginErrorWithoutUserNameAndPwd()
        {
            //Initialize Login page
            LoginPage page = new LoginPage(driver);
            page.GivenIamOnLoginPage();

            //When
            page.EnterLoginDetailsAndSubmit("", "");
            //System.Threading.Thread.Sleep(1000);
            ThenIGetErrorMessage();

            //Test report
            test = extent.CreateTest("LoginErrorWithoutUserNameAndPwd");
            //test.Log(Status.Pass, "TestPassed");
        }

        [Test]
        public void LoginErrorWithInvalidUserNameAndPwd()
        {
            //Initialize Login page
            LoginPage page = new LoginPage(driver);
            page.GivenIamOnLoginPage();
            //System.Threading.Thread.Sleep(40000);

            //When
            page.EnterLoginDetailsAndSubmit("InvalidU", "InvalidP");
            //System.Threading.Thread.Sleep(3000);
            ThenAnErrorMessagePopsUp();

            //Test report
            test = extent.CreateTest("LoginErrorWithInvalidUserNameAndPwd");
           // test.Log(Status.Pass, "TestPassed");
        }
        [Test]
        public void TestLogout()
        {
            //Initialize Admin page
            AdminHomePage page = new AdminHomePage(driver);
            //When
            page.LogOut();
            //Then
            Assert.AreEqual("NAIT - Login", driver.Title);
            //Test report
            test = extent.CreateTest("TestLogout");
           // test.Log(Status.Pass, "TestPassed");
        }

        [Test]
        public void TestLogoutFail()
        {
            //Initialize Admin page
            AdminHomePage page = new AdminHomePage(driver);
            //When
            page.LogOut();
            //Then
            Assert.AreEqual("NAIT Admin", driver.Title);
            //Test report
            testFail = extent.CreateTest("TestLogoutFail");
            testFail.Log(Status.Fail, "TestFailed");
        }

        public void ThenIGetErrorMessage()
        {
            driver.FindElement(By.Id("txtUserName-error"));
            driver.FindElement(By.Id("txtPassword-error"));
            //System.Threading.Thread.Sleep(1000);
        }

        public void ThenAnErrorMessagePopsUp()
        {
            driver.FindElement(By.ClassName("message_content"));
        }

        private IWebDriver driver;
        private WebDriverWait Wait;
        public ExtentReports extent;
        public ExtentTest test;
        public ExtentTest testFail;

        [OneTimeSetUp]
        public void setUpOnce()
        {
            var htmlReporter = new ExtentHtmlReporter("extentreport.html");
            htmlReporter.Configuration().Theme = Theme.Dark;
            //htmlReporter.Configuration().DocumentTitle = "Test Report";
            //htmlReporter.Configuration().ReportName = "Test Report";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Operationg System:", "Windows 10");
            //var test = extent.CreateTest("TestLogin");
            //var test = extent.CreateTest("TestLogoutFail");
        }

        [SetUp]
        public void SetUp()
        { 
            driver = new TWebDriver();
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Close()
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "+ TestContext.CurrentContext.Result.StackTrace +";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            if (status == TestStatus.Failed)
            {
                test.Log(Status.Fail, status + errorMessage);
            }
            extent.Flush();
            driver.Close();
           
        }

        //[OneTimeTearDown]
        //public void  OneTimeTearDown()
        //{
        //    extent.Flush();
        //}
    }
}
