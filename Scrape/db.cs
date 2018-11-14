using System;
using System.Collections.Generic;
using Npgsql;

namespace Scrape
{
    public class db
    {
        public string connString { get; set; }

        public db() {
            connString = "Host=localhost;Username=cmilkaitis;Database=cmilkaitis";
        }

        public int getBatchId() {
            var batchId = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT MAX(scrape_id) FROM public.stocks", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        batchId = reader.GetInt32(0);
                conn.Close();
            }
            
            return batchId;
        }

        public void saveStock(IList<Stock> stockList)
        {
            var batchId = getBatchId() + 1;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                foreach (var stock in stockList)
                {
                    using (var cmd = new NpgsqlCommand("INSERT INTO public.stocks (scrape_id, ticker, last_price, change, percent_change, market_time, volume, market_cap) VALUES (@s, @t, @l, @c, @p, @m, @v, @z)", conn))
                    {

                        cmd.Parameters.AddWithValue("s", batchId);
                        cmd.Parameters.AddWithValue("t", stock.Ticker);
                        cmd.Parameters.AddWithValue("l", stock.LastPrice);
                        cmd.Parameters.AddWithValue("c", stock.Change);
                        cmd.Parameters.AddWithValue("p", stock.PercentChange);
                        cmd.Parameters.AddWithValue("m", stock.MarketTime);
                        cmd.Parameters.AddWithValue("v", stock.Volume);
                        cmd.Parameters.AddWithValue("z", stock.MarketCap);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();

                // Retrieve all rows
               
            }
        }

    }
}
