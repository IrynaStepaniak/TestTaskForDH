using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net;

namespace TestSuite
{
    class WebTests
    {
        IWebDriver driver;
        const String URL = "http://uitest.duodecadits.com/";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = URL;
        }

        [Test]
        public void REQ_UI_1_VerfiyTitleCorrect()
        {
            driver.FindElement(By.Id("home")).Click();
            Assert.AreEqual("UI Testing Site", driver.Title);
        }
        [Test]
        public void REQ_UI_2_VerifyCompanyLogoPresence()
        {
            IWebElement logo = driver.FindElement(By.Id("dh_logo"));
            Assert.NotNull(logo);
            driver.FindElement(By.Id("form")).Click();
            logo = driver.FindElement(By.Id("dh_logo"));
            Assert.NotNull(logo);
        }

        [Test]
        public void REQ_UI_3_VerfiyOnHomePage()
        {
            driver.FindElement(By.Id("home")).Click();
            Assert.AreEqual(URL, driver.Url);

        }
        [Test]
        public void REQ_UI_5_VerifyOnFormPage()
        {
            driver.FindElement(By.Id("form")).Click();
            Assert.AreNotEqual(URL, driver.Url);
            IWebElement formtext = driver.FindElement(By.XPath("/html/body/div[@class='container']/div[@class='ui-test']/h1"));
            Assert.AreEqual("Simple Form Submission", formtext.Text);
        }
        [Test]
        public void REQ_UI_4_VerfiyHomeElementActive()
        {
            IWebElement homeWebElement = driver.FindElement(By.Id("home"));
            homeWebElement.Click();
            IWebElement active = driver.FindElement(By.XPath(" /html/body/nav[@class = 'navbar navbar-inverse navbar-fixed-top'] / div[@class = 'container'] / div[@id = 'navbar'] / ul[@class = 'nav navbar-nav']/li[@class = 'active']"));
            Assert.AreEqual("Home", active.Text);
        }
        [Test]
        public void REQ_UI_6_VerifyFormElementActive()
        {
            IWebElement formWebElement = driver.FindElement(By.Id("form"));
            formWebElement.Click();
            IWebElement active = driver.FindElement(By.XPath("//div[@class='container']/div[@id='navbar']/ul[@class='nav navbar-nav']/li[@class='active']/a[@id='form']"));
            Assert.AreEqual("Form", active.Text);
        }
        [Test]
        public void REQ_UI_8_VerifyUITestingHome()
        {
            IWebElement uiTesting = driver.FindElement(By.Id("site"));
            uiTesting.Click();
            IWebElement active = driver.FindElement(By.XPath(" /html/body/nav[@class = 'navbar navbar-inverse navbar-fixed-top'] / div[@class = 'container'] / div[@id = 'navbar'] / ul[@class = 'nav navbar-nav']/li[@class = 'active']"));
            Assert.AreEqual("Home", active.Text);
            Assert.AreEqual(URL, driver.Url);
        }
        [Test]
        public void REQ_UI_9_10_VerifyTextOnHomePage()
        {
            IWebElement welcomeText = driver.FindElement(By.TagName("h1"));
            Assert.AreEqual("Welcome to the Docler Holding QA Department", welcomeText.Text);
            IWebElement secondaryText = driver.FindElement(By.TagName("p"));
            Assert.AreEqual("This site is dedicated to perform some exercises and demonstrate automated web testing.", secondaryText.Text);
        }

        [Test]
        public void REQ_UI_11_VerifyFormInputAndSubmit()
        {
            driver.FindElement(By.Id("form")).Click();
            IWebElement input = driver.FindElement(By.Id("hello-input"));
            Assert.NotNull(input);
            IWebElement submit = driver.FindElement(By.Id("hello-submit"));
            Assert.NotNull(submit);
        }
        [Test]
        public void REQ_UI_12_VerifyResultsForm()
        {
            driver.FindElement(By.Id("form")).Click();
            string[] array = new string[4];
            array[0] = "John";
            array[1] = "Sofia";
            array[2] = "Charlie";
            array[3] = "Emily";
            for (int i = 0; i < array.Length; i++)
            {
                string a = array[i];
                IWebElement inputField = driver.FindElement(By.XPath("//div[@class='row']/div[@class='col-md-4'][2]/div[@class='input-group']/input[@id='hello-input']"));
                Assert.NotNull(inputField);
                inputField.Clear();
                inputField.SendKeys(a);
                IWebElement submitValue = driver.FindElement(By.Id("hello-submit"));
                submitValue.Click();
                Assert.AreEqual("/hello.html", new Uri(driver.Url).LocalPath);
                IWebElement helloText = driver.FindElement(By.Id("hello-text"));
                Assert.AreEqual("Hello " + a + "!", helloText.Text);
                driver.Navigate().Back();
            }

        }

        [Test]
        public void REQ_UI_7_Verfiy404OnErrorClick()
        {
            driver.FindElement(By.Id("error")).Click();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(driver.Url);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Assert.Fail();
            }
            catch (WebException we)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, ((HttpWebResponse)we.Response).StatusCode);
            }

        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }

    }
}

