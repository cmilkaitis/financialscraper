namespace Scrape
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string PercentChange { get; set; }
        public string MarketTime { get; set; }
        public string Volume { get; set; }
        public string MarketCap { get; set; }

        public Stock(string ticker, string lastPrice, string change, string percentChange, string marketTime, string volume, string marketCap)
        {
            Ticker = ticker;
            LastPrice = lastPrice;
            Change = change;
            PercentChange = percentChange;
            MarketTime = marketTime;
            Volume = volume;
            MarketCap = marketCap;
        }
    }
}
