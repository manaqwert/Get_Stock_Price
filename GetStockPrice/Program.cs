using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

class Program
{
    static void Main(string[] args)
    {
        string date = DateTime.Now.ToString("yyyyMMdd");
        string url = $"https://info.finance.yahoo.co.jp/ranking/?kd=4&tm=d&mk=1&tmu={date}&vl=20&p=1";
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);
        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='rankingTabledata yjM']");
        string filePath = "stock_prices_" + date + ".txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (HtmlNode node in nodes)
            {
                string code = node.SelectSingleNode(".//span[@class='rankTabledata yjMSt']")?.InnerText;
                string name = node.SelectSingleNode(".//a[@class='yjM']")?.InnerText;
                string open = node.SelectSingleNode(".//span[@class='stoksPrice']")?.InnerText;
                string[] prices = node.SelectSingleNode(".//td[@class='price']")?.InnerText
                    .Split(new char[] { '～', '（', '）', '株' }, StringSplitOptions.RemoveEmptyEntries);
                if (prices != null && prices.Length >= 4)
                {
                    string high = prices[0];
                    string low = prices[1];
                    string close = prices[2];
                    string volume = prices[3];
                    writer.WriteLine($"{code},{name},{open},{high},{low},{close},{volume}");
                }
            }
        }
    }
}


/*
using System;
using System.Net;
using HtmlAgilityPack;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Webサイトからデータを取得する
        WebClient webClient = new WebClient();
        string html = webClient.DownloadString("https://www.jpx.co.jp/markets/statistics-equities/misc/01.html");

        // HtmlAgilityPackを使用してHTMLを解析する
        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        document.LoadHtml(html);

        // 銘柄情報を取得する
        var stockDataList = new List<StockData>();
        var stockDataRows = document.DocumentNode.SelectNodes("//table[@class='l-table-table']//tr");

        // ヘッダー行を除外する
        stockDataRows.RemoveAt(0);

        // 各銘柄の情報を取得する
        foreach (var row in stockDataRows)
        {
            var stockData = new StockData();
            var columns = row.SelectNodes(".//td");

            // 銘柄コード
            stockData.Code = columns[0].InnerText.Trim();

            // 銘柄名
            stockData.Name = columns[1].InnerText.Trim();

            // 終値
            stockData.ClosingPrice = int.Parse(columns[2].InnerText.Trim().Replace(",", ""));

            // 最高値
            stockData.HighPrice = int.Parse(columns[3].InnerText.Trim().Replace(",", ""));

            // 最安値
            stockData.LowPrice = int.Parse(columns[4].InnerText.Trim().Replace(",", ""));

            // 出来高
            stockData.TradedVolume = int.Parse(columns[5].InnerText.Trim().Replace(",", ""));

            // 始値
            stockData.OpeningPrice = int.Parse(columns[6].InnerText.Trim().Replace(",", ""));

            // 銘柄情報をリストに追加する
            stockDataList.Add(stockData);
        }

        // 取得した銘柄情報を表示する
        foreach (var stockData in stockDataList)
        {
            Console.WriteLine("{0} {1} {2} {3} {4} {5}", stockData.Code, stockData.Name, stockData.ClosingPrice, stockData.HighPrice, stockData.LowPrice, stockData.TradedVolume, stockData.OpeningPrice);
        }
    }

    class StockData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TradedVolume { get; set; }
        public int OpeningPrice { get; set; }
        public int ClosingPrice { get; set; }
        public int HighPrice { get; set; }
        public int LowPrice { get; set; }
    }
}
/*

/*
using System;
using System.Net;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace GetTradingResult
{
    class Program
    {
        static void Main(string[] args)
        {
            // Webサイトからデータを取得する
            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("https://www.jpx.co.jp/markets/statistics-equities/misc/01.html");

            // HtmlAgilityPackを使用してHTMLを解析する
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            // 銘柄情報を取得する
            var stockDataList = new List<StockData>();
            var stockDataRows = document.DocumentNode.SelectNodes("//table[@class='l-table-table']//tr");

            // ヘッダー行を除外する
            stockDataRows.RemoveAt(0);

            // 各銘柄の情報を取得する
            foreach (var row in stockDataRows)
            {
                var stockData = new StockData();
                var columns = row.SelectNodes(".//td");

                // 銘柄コード
                stockData.Code = columns[0].InnerText.Trim();

                // 銘柄名
                stockData.Name = columns[1].InnerText.Trim();

                // 終値
                stockData.ClosingPrice = int.Parse(columns[2].InnerText.Trim().Replace(",", ""));

                // 最高値
                stockData.HighPrice = int.Parse(columns[3].InnerText.Trim().Replace(",", ""));

                // 最安値
                stockData.LowPrice = int.Parse(columns[4].InnerText.Trim().Replace(",", ""));

                // 出来高
                stockData.TradedVolume = int.Parse(columns[5].InnerText.Trim().Replace(",", ""));

                // 始値
                stockData.OpeningPrice = int.Parse(columns[6].InnerText.Trim().Replace(",", ""));

                // 銘柄情報をリストに追加する
                stockDataList.Add(stockData);
            }

            // 取得した銘柄情報を表示する
            foreach (var stockData in stockDataList)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", stockData.Code, stockData.Name, stockData.ClosingPrice, stockData.HighPrice, stockData.LowPrice, stockData.TradedVolume, stockData.OpeningPrice);
            }
        }

        class StockData
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public int ClosingPrice { get; set; }
            public int HighPrice { get; set; }
            public int LowPrice
            {
                get;


            }
        }
    }
}
*/



