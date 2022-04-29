using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xunit;


namespace Testing
{
    public class baseTest
    {
        [Fact]
        public void main()
        {
            //Initialize User A and navigate to site
            var userA = new ChromeDriver();
            userA.Manage().Window.Maximize();
            userA.Navigate().GoToUrl("https://paychex-story-point-estimator.azurewebsites.net/");

            //Initialize User B
            var userB = new ChromeDriver();
            userB.Manage().Window.Maximize();
            userB.Navigate().GoToUrl("https://paychex-story-point-estimator.azurewebsites.net/");

            createRoom(userA);
            joinRoom(userB);

            vote(userB);
            Thread.Sleep(500);

            checkVotesAndUnhide(userA);
            clearVotesAndCheck(userA);



            userA.FindElements(By.ClassName("admin-tools-buttons"))[4].Click();
            Thread.Sleep(1000);
            userA.FindElement(By.TagName("html")).SendKeys(Keys.Tab + Keys.Enter);
            Thread.Sleep(3000);
            userA.Close();
            userB.Close();
        }

        public void createRoom(ChromeDriver driver)
        {
            //Click on create room button and assert that the navigation executed correctly
            driver.FindElement(By.LinkText("Create Room")).Click();
            Thread.Sleep(500);
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);

            //Attempt an empty submission, which should fail
            //Confirm failure by asserting that the browser did not redirect, and is still on create room page
            driver.FindElement(By.TagName("button")).Click();
            Thread.Sleep(500);
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);

            //Attempt a submission in which the room name and username are both one character, which is too short for both fields
            foreach (IWebElement textbox in driver.FindElements(By.ClassName("join-create-text-fields")))
            {
                textbox.SendKeys("a");
                Thread.Sleep(500);
            }

            //Attempting to submit the data (should fail)
            driver.FindElement(By.TagName("button")).Click();
            Thread.Sleep(500);
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);

            //Attempting submission with valid username but invalid room name
            driver.FindElements(By.ClassName("join-create-text-fields"))[0].SendKeys(Keys.Backspace + "userA");
            Thread.Sleep(500);
            driver.FindElements(By.ClassName("join-create-text-fields"))[1].SendKeys(Keys.Backspace + "a");
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);
            Thread.Sleep(500);

            //Attempting valid submission and asserting navigation
            driver.FindElements(By.ClassName("join-create-text-fields"))[1].SendKeys(Keys.Backspace + "TestingRoom");
            driver.FindElement(By.ClassName("mud-checkbox")).Click();
            driver.FindElement(By.TagName("button")).Click();
            Thread.Sleep(500);
            Assert.NotEqual("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);
        }

        public void joinRoom(ChromeDriver driver)
        {
           
            driver.FindElement(By.LinkText("Join Room")).Click();
            Thread.Sleep(500);

            //Empty submission
            driver.FindElements(By.TagName("button"))[1].Click();
            
            //Filling in username
            Thread.Sleep(200);
            driver.FindElement(By.TagName("input")).SendKeys("UserB");
            Thread.Sleep(500);
            
            //Submission with only username
            driver.FindElements(By.TagName("button"))[1].Click();
            
            //Locating dropdown and selecting the room that was just created by User A
            var select = new SelectElement(driver.FindElement(By.TagName("select")));
            select.SelectByText("TestingRoom");
            Thread.Sleep(500);

            //Valid submission, confirming redirection
            driver.FindElements(By.TagName("button"))[1].Click();
            Thread.Sleep(200);
            Assert.NotEqual("https://paychex-story-point-estimator.azurewebsites.net/JoinRoom", driver.Url);
        }

        public void vote(ChromeDriver driver)
        {
            driver.FindElements(By.ClassName("tool-buttons"))[1].Click();            
        }

        public void checkVotesAndUnhide(ChromeDriver driver)
        {
            var table_vote_elements = driver.FindElements(By.CssSelector("p.table-vote-text"));
            Assert.Equal(2, table_vote_elements.Count);

            driver.FindElements(By.ClassName("admin-tools-buttons"))[0].Click();
            Thread.Sleep(500);
            table_vote_elements = driver.FindElements(By.CssSelector("p.table-vote-text"));
            Assert.Equal(3, table_vote_elements.Count);
        }

        public void clearVotesAndCheck(ChromeDriver driver)
        {
            var table_vote_elements = driver.FindElements(By.CssSelector("p.table-vote-text"));
            Assert.Equal(3, table_vote_elements.Count);

            driver.FindElements(By.ClassName("admin-tools-buttons"))[2].Click();
            Thread.Sleep(500);
            driver.FindElement(By.TagName("html")).SendKeys(Keys.Tab + Keys.Enter);
            Thread.Sleep(500);
            table_vote_elements = driver.FindElements(By.CssSelector("p.table-vote-text"));
            Assert.Equal(2, table_vote_elements.Count);
        }
    }
}
