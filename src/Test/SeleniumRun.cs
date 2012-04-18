using System;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using Website.App;

namespace Test
{
    [TestFixture]
    public class SeleniumRun
    {
        [Test]
        [Repeat(10)]
        public void LoadPage()
        {
            var driver = new FirefoxDriver();
            driver.Url = "http://www.reallysimpleabtesting.co.uk//";

            var element = driver.FindElementByClassName("test");

            var message = element.Text;

            Console.WriteLine(message);

            if (message.Contains("call to action"))
            {
                if (Chance.FlipACoin())
                {
                    var link = driver.FindElementById("calltoaction");
                    link.Click();
                }
            }

            driver.Close();
        }
    }
}
