using System;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace UITest
{
    [TestFixture]
    public class SeleniumRun
    {
        [Test]
        [Repeat(10)]
        public void LoadPage()
        {
            var driver = new FirefoxDriver();
            driver.Url = "http://localhost:26274/";

            var element = driver.FindElementByClassName("test");

            Console.WriteLine(element.Text);

            driver.Close();
        }
    }
}
