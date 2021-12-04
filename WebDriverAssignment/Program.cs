using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverAssignment
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //create reference for browser  
            IWebDriver driver = new ChromeDriver();
            // navigate to carlist url
            driver.Navigate().GoToUrl("https://www.carlist.my/");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            //maximise the window
            driver.Manage().Window.Maximize();
            //find the element where we have to enter the search keyword
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.selectize-input input[placeholder*='What car are you']")));
            IWebElement searchField = driver.FindElement(By.CssSelector("div.selectize-input input[placeholder*='What car are you']"));
            //enter the word Used in the search field
            searchField.SendKeys("Used");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.selectize-input input[placeholder*='What car are you']")));
            searchField.SendKeys(Keys.Tab);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.search-button button")));
            //click on the search button
            IWebElement searchButton = driver.FindElement(By.CssSelector("div.search-button button"));
            searchButton.Click();
            
            //wait until page loads and get the list of all cars available
            IWebElement searchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("ul.pagination li[class='next']")));
            IList<IWebElement> listItems = driver.FindElements(By.CssSelector("h2.listing__title a"));

            for (int i = 0; i < listItems.Count; i++)
            {
                //select the first car from the list
                listItems.ElementAt(0).Click();
                break;

            }
            //wait until the page loads and get the price of the car
            IWebElement price = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.listing__item-price h3")));
            string carPrice = driver.FindElement(By.CssSelector("div.listing__item-price h3")).Text;
            String actualPrice = carPrice.Substring(3);
            String updatedPrice = actualPrice.Replace(",", "");
            //convert the string price to integer
            int intPrice = int.Parse(updatedPrice);
            try
            {
                //Assert whether the car price is greater than RM 1000
                Assert.IsTrue(intPrice > 1000, "Car Price is greater than RM 1000. Actual Price = " + "RM" + intPrice);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            driver.Quit();
            Console.WriteLine("test case ended");

        }
    }
}
