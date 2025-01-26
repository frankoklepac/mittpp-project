using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace ProjektniZadatak
{
    public class BaseTest
    {
#pragma warning disable NUnit1032 
        protected IWebDriver driver;
#pragma warning restore NUnit1032 
        protected StringBuilder verificationErrors;
        protected string baseURL;
        protected bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver(); 
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
            }
        }

        protected void UserLogin(string email, string password)
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
            driver.FindElement(By.LinkText("Log in")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
        }
    }

    [TestFixture, Order(1)]
    public class RegisterTest : BaseTest
    {
        [Test, Order(1)]
        public void Register()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("FirstName")).Click();
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys("Franko");
            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("Klepac");
            string uniqueEmail = $"fklepac+{Guid.NewGuid()}@etfos.hr";
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(uniqueEmail);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("test123");
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("test123");
            driver.FindElement(By.Id("gender-male")).Click();
            driver.FindElement(By.Id("register-button")).Click();
            driver.FindElement(By.XPath("//input[@value='Continue']")).Click();
        }
    }

    [TestFixture, Order(2)]
    public class LoginTest : BaseTest
    {
        [Test, Order(2)]
        public void Login()
        {
            UserLogin("fklepac@etfos.hr", "test123");
        }
    }
    [TestFixture, Order(3)]
    public class  LogoutTest : BaseTest
    {
        [Test, Order(3)]
        public void Logout()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.LinkText("Log out")).Click();
        }
    }

    [TestFixture, Order(4)]
    public class AddToCartTest : BaseTest
    {
        [Test, Order(4)]
        public void AddToCart()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.LinkText("Computers")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Computers'])[5]/following::a[1]")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//input[@value='Add to cart']")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("add-to-cart-button-72")).Click();
        }

    }

    [TestFixture, Order(5)]
    public class SearchAndFilterTest : BaseTest
    {
        [Test, Order(5)]
        public void SearchAndFilter()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.Id("small-searchterms")).Click();
            driver.FindElement(By.Id("small-searchterms")).Clear();
            driver.FindElement(By.Id("small-searchterms")).SendKeys("computer");
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("//input[@value='Search']")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("products-orderby")).Click();
            new SelectElement(driver.FindElement(By.Id("products-orderby"))).SelectByText("Price: High to Low");
            Thread.Sleep(1500);
            driver.FindElement(By.Id("products-pagesize")).Click();
            new SelectElement(driver.FindElement(By.Id("products-pagesize"))).SelectByText("12");
            Thread.Sleep(1500);
            driver.FindElement(By.Id("products-viewmode")).Click();
            new SelectElement(driver.FindElement(By.Id("products-viewmode"))).SelectByText("List");
        }
    }

    [TestFixture, Order(6)]
    public class SearchAndAddToCartTest : BaseTest
    {
        [Test, Order(6)]
        public void SearchAndAddToCart()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.Id("small-searchterms")).Click();
            driver.FindElement(By.Id("small-searchterms")).Clear();
            driver.FindElement(By.Id("small-searchterms")).SendKeys("internet");
            driver.FindElement(By.Id("small-searchterms")).SendKeys(Keys.Enter);
            Thread.Sleep(1500);
            driver.FindElement(By.LinkText("Computing and Internet")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.Id("add-to-cart-button-13")).Click();
        }
    }

    [TestFixture, Order(7)]
    public class AddNewAddressTest : BaseTest
    {
        [Test, Order(7)]
        public void AddNewAddress()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.LinkText("fklepac@etfos.hr")).Click();
            driver.FindElement(By.LinkText("Addresses")).Click();
            driver.FindElement(By.XPath("//input[@value='Add new']")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("Address_FirstName")).Click();
            driver.FindElement(By.Id("Address_FirstName")).Click();
            driver.FindElement(By.Id("Address_FirstName")).Clear();
            driver.FindElement(By.Id("Address_FirstName")).SendKeys("Marko");
            driver.FindElement(By.Id("Address_LastName")).Clear();
            driver.FindElement(By.Id("Address_LastName")).SendKeys("Markovic");
            driver.FindElement(By.Id("Address_Email")).Clear();
            driver.FindElement(By.Id("Address_Email")).SendKeys("marko@etfos.hr");
            driver.FindElement(By.Id("Address_CountryId")).Click();
            new SelectElement(driver.FindElement(By.Id("Address_CountryId"))).SelectByText("United States");
            driver.FindElement(By.Id("Address_City")).Click();
            driver.FindElement(By.Id("Address_City")).Clear();
            driver.FindElement(By.Id("Address_City")).SendKeys("New York");
            driver.FindElement(By.Id("Address_Address1")).Clear();
            driver.FindElement(By.Id("Address_Address1")).SendKeys("New York 001");
            driver.FindElement(By.Id("Address_ZipPostalCode")).Click();
            driver.FindElement(By.Id("Address_ZipPostalCode")).Clear();
            driver.FindElement(By.Id("Address_ZipPostalCode")).SendKeys("54452");
            driver.FindElement(By.Id("Address_PhoneNumber")).Click();
            driver.FindElement(By.Id("Address_PhoneNumber")).Clear();
            driver.FindElement(By.Id("Address_PhoneNumber")).SendKeys("0995437654");
            driver.FindElement(By.XPath("//input[@value='Save']")).Click();
        }
    }

    [TestFixture, Order(8)]
    public class RemoveAddressTest : BaseTest
    {
        [Test, Order(8)]
        public void RemoveAdress()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.LinkText("fklepac@etfos.hr")).Click();
            acceptNextAlert = true;
            driver.FindElement(By.LinkText("Addresses")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.That(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure[\\s\\S]$"));
        }
        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
    [TestFixture, Order(9)]
    public class AddToCartPlaceOrderTest : BaseTest
    {
        [Test, Order(9)]
        public void AddToCartPlaceOrder()
        {
            UserLogin("fklepac@etfos.hr", "test123");
            driver.FindElement(By.LinkText("Computers")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Computers'])[5]/following::a[1]")).Click();
            driver.FindElement(By.XPath("//input[@value='Add to cart']")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("add-to-cart-button-72")).Click();
            driver.FindElement(By.LinkText("Electronics")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Camera, photo'])[4]/following::a[2]")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.LinkText("Smartphone")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("addtocart_43_EnteredQuantity")).Click();
            driver.FindElement(By.Id("addtocart_43_EnteredQuantity")).Clear();
            driver.FindElement(By.Id("addtocart_43_EnteredQuantity")).SendKeys("2");
            driver.FindElement(By.Id("product-details-form")).Submit();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//li[@id='topcartlink']/a/span")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.Id("termsofservice")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.Id("checkout")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//input[@value='Continue']")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//div[@id='shipping-buttons-container']/input")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//div[@id='shipping-method-buttons-container']/input")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//div[@id='payment-method-buttons-container']/input")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//div[@id='payment-info-buttons-container']/input")).Click();
            Thread.Sleep(250);
            driver.FindElement(By.XPath("//input[@value='Confirm']")).Click();
            Thread.Sleep(250);
        }
    }
}