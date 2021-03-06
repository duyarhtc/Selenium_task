﻿
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace SeleniumTestInsiderProject
{


    [TestFixture]
    public class SeleniumTest
    {
        public IWebDriver driver;
        Verfying verfying;

        [SetUp]
        public void Init()
        {
           
            driver = new ChromeDriver();
            
        }

        //Task 1 : access to the site
        [Test]
        public void AccessSiteStart()
        {
            driver.Navigate().GoToUrl("https://www.amazon.com");
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(2000);
            String currentURL = driver.Url;
            verfying = new Verfying();
            if (verfying.HttpRequestStatusCode(currentURL) == true)
            {
                Console.WriteLine("Page uploaded.");
                System.Threading.Thread.Sleep(2000);
                LoginTest();
            }
            else Console.WriteLine("Page not displayed.");

        }

        //Task 2 : Login
        [Test]
        public void LoginTest()
        {
            driver.FindElement(By.Id("nav-tools")).Click();           
            driver.FindElement(By.Id("ap_email")).SendKeys("seleniuminsider@gmail.com");
            driver.FindElement(By.Id("ap_password")).SendKeys("Hatice_06");
            driver.FindElement(By.Id("signInSubmit")).Click();//Siteye kayıtlı Kullancının önceden giriş yapmış olduğu ve  doğrulama adımlarını tamamladığı varsayılmaktadır.Doğrulama code mail adresi=seleniuminsider@gmail.com / şifre:Hatice_06
            System.Threading.Thread.Sleep(3000);
            SearchSamsung();
        }

        //Task 3-4:  Samsung search and approve
        [Test]
        public void SearchSamsung()
        {
            driver.FindElement(By.ClassName("nav-searchbar")).Click();
            driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys("samsung");
            //System.Threading.Thread.Sleep(4000);
            IWebElement container = driver.FindElement(By.Id("suggestions"));
            IReadOnlyCollection<IWebElement> menulist = container.FindElements(By.ClassName("s-suggestion"));
            if (menulist.Count > 0)
            {
                Console.WriteLine("Product is/are listed.");

            }
            else Console.WriteLine("Product not found.");
            driver.FindElement(By.ClassName("nav-input")).SendKeys(Keys.Enter);            
            DisplayPage();

        }
        

        //Task 5: Display page 2
        [Test]
        public void DisplayPage()
        {
            String previousURL = driver.Url;
            driver.FindElement(By.CssSelector("ul.a-pagination")).FindElement(By.LinkText("2")).Click();
            System.Threading.Thread.Sleep(2000);
            String currentURL = driver.Url;
            verfying = new Verfying();
            
                //page control
                if (previousURL != currentURL && verfying.HttpRequestStatusCode(currentURL) == true)
            {
                Console.WriteLine("Page 2 is displayed.");

            }
            else Console.WriteLine("Page 2 is not displayed.");
            AddToList();

        }


        //Task 6 : Add to List product (3th)
        [Test]
        public void AddToList()
        {
            IWebElement element = driver.FindElement(By.XPath("//div[@data-index='2']"));
            element.Click();
            IWebElement elementt = driver.FindElement(By.XPath("//img[@data-image-index='2']"));
            elementt.Click();
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.Id("add-to-wishlist-button-submit")).Click();
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);  //close sub window 
            SelectWishList();

        }

        //Task 7 : Select Wish List
        [Test]
        public void SelectWishList()
        {

            IWebElement element= driver.FindElement(By.ClassName("nav-line-2"));
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Perform();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Id("nav-al-whishlist")).Click();

        }
         

        [TearDown]
        public void TestEnd()
        {
            driver.Quit();
        }
    }
}
