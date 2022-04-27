using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Testing
{
    public class baseTest
    {
        [Fact]
        public async void createRoomWorkflow()
        {
            //Initialize Driver and navigate to site
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://paychex-story-point-estimator.azurewebsites.net/");

            //Click on create room button and assert that the navigation executed correctly
            driver.FindElement(By.LinkText("Create Room")).Click();
            Thread.Sleep(500);
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);

            //Attempt an empty submission, which should fail
            //Confirm failure by asserting that the browser did not redirect
            driver.FindElement(By.TagName("button")).Click();
            Thread.Sleep(500);
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);

            //Attempt a submission in which the room name and username are both one character, which is too short for both fields
            foreach(IWebElement textbox in driver.FindElements(By.ClassName("join-create-text-fields")))
            {
                textbox.SendKeys("a");
                Thread.Sleep(500);
            }

            //Attempting to submit the data (should fail)
            driver.FindElement(By.TagName("button")).Click();
            Thread.Sleep(500);
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);

            //Attempting submission with valid username but invalid room name
            driver.FindElements(By.ClassName("join-create-text-fields"))[0].SendKeys(Keys.Backspace + "UserA");
            Thread.Sleep(500);
            driver.FindElements(By.ClassName("join-create-text-fields"))[1].SendKeys(Keys.Backspace + "a");
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);
            Thread.Sleep(500);

            //Attempting valid submission and asserting navigation
            driver.FindElements(By.ClassName("join-create-text-fields"))[0].SendKeys(Keys.Clear + "UserA");
            Thread.Sleep(500);
            driver.FindElements(By.ClassName("join-create-text-fields"))[1].SendKeys(Keys.Clear + "TestingRoom");
            driver.FindElement(By.ClassName("mud-checkbox")).Click();
            driver.FindElement(By.TagName("button")).Click();
            Thread.Sleep(500);
            //Assert.Contains("https://paychex-story-point-estimator.azurewebsites.net/Room", driver.Url);
            Thread.Sleep(500);

            driver.FindElements(By.ClassName("admin-tools-buttons"))[4].Click();
            Thread.Sleep(1000);
            driver.FindElement(By.TagName("html")).SendKeys(Keys.Tab + Keys.Enter);
            Thread.Sleep(5000);
            driver.Close();
        }
    }
}
