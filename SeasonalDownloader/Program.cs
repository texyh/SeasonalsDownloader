using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeasonalDownloader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var baseURL = "http://o2tvseries.com/";
            string[] seasonals = {"Suits", "The-Big-Bang-Theory-2", "The-Originals-1", "The-Vampire-Diaries-3"};
            foreach (string seasonal in seasonals)
            {
                string seasonalURL = baseURL + seasonal;
                IWebDriver webDriver = new ChromeDriver();
                webDriver.Navigate().GoToUrl(seasonalURL);
                //IWebElement webElement = webDriver.FindElement(By.Name("q"));
                //
                webDriver.Close();
            }
        }
    }
}
