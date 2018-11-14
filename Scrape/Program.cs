using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Scrape
{
    class MainClass
    {
        static void Main(string[] args)
        {


            IWebDriver driver = new ChromeDriver
            {
                Url = "https://finance.yahoo.com/portfolios"
            };

            // Set wait time for slow load
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Click login button
            driver.FindElement(By.XPath("//*[@id='uh']/header/section/div/a")).Click();

            // Submit login information
            driver.FindElement(By.Id("login-username")).SendKeys("chrismilkaitis.student@careerdevs.com" + Keys.Enter);
            driver.FindElement(By.Id("login-passwd")).SendKeys("projecttest@333" + Keys.Enter);

            // Close pop-up asking to link financial advisor info
            driver.FindElement(By.XPath("//*[@id='__dialog']/section/button")).Click();

            // Select portfolio profile
            driver.FindElement(By.XPath("//*[@id='main']/section/section/div/table/tbody/tr/td/a")).Click();

            // Store table 
            IWebElement table = driver.FindElement(By.XPath("//table[contains(@class,'_1TagL')]//tbody"));
            // Store table rows
            ICollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

            // Create List for stock objects
            IList<Stock> stockList = new List<Stock>();

            // Create then send each stock object to List
            foreach (var row in rows)
            {
                string[] data = row.Text.Split(' ');
                string[] lastEl = data[11].Split(new[] { Environment.NewLine },
                               StringSplitOptions.None);

                data[11] = lastEl[0];

                var stockObj = new Stock(data[0], data[1], data[2], data[3], data[5] + data[6] + data[7], data[10], data[11]);

                stockList.Add(stockObj);
            }

            var db = new db();
            db.saveStock(stockList);

            //foreach (var stock in stockList)
            //{
            //    Console.WriteLine($"");
            //    Console.WriteLine($"Ticker: {stock.Ticker}");
            //    Console.WriteLine($"Last Price: {stock.LastPrice}");
            //    Console.WriteLine($"Change: {stock.Change}");
            //    Console.WriteLine($"Percent Change: {stock.PercentChange}");
            //    Console.WriteLine($"Market Time: {stock.MarketTime}");
            //    Console.WriteLine($"Volume: {stock.Volume}");
            //    Console.WriteLine($"Market Cap: {stock.MarketCap}");
            //}
        }
    }
}
