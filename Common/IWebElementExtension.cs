using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Threading;
using Group1Project.TestCases;
using OpenQA.Selenium.Support.UI;

namespace Group1Project.Common
{
    public static class IWebElementExtension
    {
        public static IWebElement iwebelement;

        public static void Highlight(this IWebElement context, int duration = 2)
        {
            var rc = (RemoteWebElement)context;
            var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            var script = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: #ff0000""; ";
            driver.ExecuteScript(script, rc);
            Thread.Sleep(1000 * duration);
            var clear = @"arguments[0].style.cssText = ""border-width: 0px; border-style: solid; border-color: #ff0000""; ";
            driver.ExecuteScript(clear, rc);
        }

        public static void Blink(this IWebElement context, int times = 1)
        {
            int loop = 0;
            var rc = (RemoteWebElement)context;
            var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            var script1 = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: #ff0000""; ";
            var script2 = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: #00ff00""; ";
            var clear = @"arguments[0].style.cssText = ""border-width: 0px; border-style: solid; border-color: #ff0000""; ";
            do
            {
                driver.ExecuteScript(script2, rc);
                Thread.Sleep(250);
                driver.ExecuteScript(script1, rc);
                Thread.Sleep(250);
                loop++;
            } while (loop < times);
            driver.ExecuteScript(clear, rc);
        }
        public static void Set(this IWebElement element, string value, bool clearFirst)
        {
            if (clearFirst) element.Clear();
            element.SendKeys(value);
        }

        public static IWebElement FindElement(By by)
        {                       
            try
            {
                iwebelement = TestCases.Testbase.WebDriver.FindElement(by);
                if (Constant.DebugMode)
                    iwebelement.Blink();
                return iwebelement;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
               
        
    }
}
