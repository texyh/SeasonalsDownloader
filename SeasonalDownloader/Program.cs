using System;
using System.Data;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace SeasonalDownloader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string videoFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            var baseURL = "http://o2tvseries.com/";
            string[] seasonals = {"Suits", "The-Big-Bang-Theory-2", "The-Originals-1", "The-Vampire-Diaries-3"};
            IWebDriver webDriver = new ChromeDriver();
            foreach (string seasonal in seasonals)
            {
                string seasonalURL = baseURL + seasonal;
                webDriver.Navigate().GoToUrl(seasonalURL);
                //
                string seasonalTitle = webDriver.FindElement(By.ClassName("active")).Text;
                string seasonalFolder = Path.Combine(videoFolder, seasonalTitle);
                //
                var latestSeason = webDriver.FindElements(By.PartialLinkText("Season ")).FirstOrDefault();
                latestSeason?.Click();
                //
                var episodeLinks =
                    webDriver.FindElements(By.PartialLinkText("Episode ")).Select(x => x.GetAttribute("href"));
                foreach (var episodeLink in episodeLinks)
                {
                    webDriver.Navigate().GoToUrl(episodeLink);
                    var downloadLink = webDriver.FindElement(By.PartialLinkText(".mp4"));
                    //
                    string fileName = downloadLink.Text;
                    string fullFilePath = Path.Combine(seasonalFolder, fileName);
                    if (!File.Exists(fullFilePath))
                    {
                        downloadLink.Click();
						webDriver.Navigate().Back();
                    }
                    else
                    {
                        break;
                    }
                }
            }
			webDriver.Close();
			Console.WriteLine("Done.");
        }
    }
}
