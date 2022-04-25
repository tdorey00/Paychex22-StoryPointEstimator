using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Testing
{
    public class createRoom
    {
        private ChromeDriver driver = new ChromeDriver();

        [Fact]
        public void createRoomNav()
        {
            driver.Navigate().GoToUrl("https://paychex-story-point-estimator.azurewebsites.net/");
            driver.FindElement(By.LinkText("Create Room")).Click();
            Assert.Equal("https://paychex-story-point-estimator.azurewebsites.net/CreateRoom", driver.Url);
            driver.FindElement(By.TagName("button")).Click();
        }
        public void emptySubmit()
        {
           
        }
    }
}
