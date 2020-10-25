    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace StockTracker
    {
        class StockTracker
        {
            static void Main(string[] args)
            {
                StockTracker maxProfittracker = new StockTracker();
                maxProfittracker.maxProfit();
            }
            public void maxProfit()
            {
                // validate stock availability 
                if (Stocks.Count() != 0)
                {
                    // getLowestPricedStock
                    Stock lowestPricedStock = getLowPricedStock();
                    // getHigestPricedStock
                    Stock highestPricedStock = getHighPricedStock(lowestPricedStock);
                    //calculateProfit
                    Double maxProfit = highestPricedStock.highPrice - lowestPricedStock.lowPrice;
                    Console.WriteLine(" #Profit {0}", maxProfit);
                }
                else
                {
                    Console.WriteLine("Sorry!!! No stocks available to calculate max profit");
                }
            }
            private Stock getLowPricedStock()
            {

                //find low priced stock
                var lowPrice = Stocks.Min(s => s.lowPrice);
                var lowPricedStockQuery =
                    from lowPricedStock in Stocks.Distinct()
                    where lowPricedStock.lowPrice == lowPrice
                    select lowPricedStock;

                //find highestPrice
                foreach (Stock lowPricedStock in lowPricedStockQuery)
                {
                    Console.Write("Buy at Day: {0}, Low: {1}, High: {2}", lowPricedStock.day, lowPricedStock.lowPrice, lowPricedStock.highPrice);
                }

                return lowPricedStockQuery.FirstOrDefault();
            }
            private Stock getHighPricedStock(Stock lowPricedStock)
            {
                //find higest priced stock after the lowest price stock day
                var stocksAfterbuyDayQuery = 
                    from stockAfterBuy in Stocks.Distinct()
                    where stockAfterBuy.day >= lowPricedStock.day
                    select stockAfterBuy;

                var highPrice = 0;
                if (stocksAfterbuyDayQuery.Count() > 1)
                {
                    highPrice = stocksAfterbuyDayQuery.ToList().Distinct().Max(s => s.highPrice);
                }
                else
                {
                    highPrice = stocksAfterbuyDayQuery.FirstOrDefault().highPrice;
                }

                var highPriceStockQuery =
                    from highPricedStock in stocksAfterbuyDayQuery.ToList().Distinct()
                    where highPricedStock.highPrice == highPrice
                    select highPricedStock;

                foreach (Stock highPricedStock in highPriceStockQuery)
                {
                    Console.Write(" #Sell at Day: {0} , Low: {1}, High: {2}", highPricedStock.day, highPricedStock.lowPrice, highPricedStock.highPrice);
                }

                return highPriceStockQuery.FirstOrDefault();
            }
            public class Stock
            {
                public int day { get; set; }
                public int lowPrice { get; set; }
                public int highPrice { get; set; }

                //ensure no duplacate stocks based on low and high prices
                public override bool Equals(Object stockObj){
                    if (!(stockObj is Stock))
                        return false;
                    Stock stock = (Stock) stockObj;
                    return (stock.lowPrice == this.lowPrice && stock.highPrice == this.highPrice);
                }
                public override int GetHashCode()
                {
                    return String.Format("{0}|{1}", lowPrice,highPrice).GetHashCode();
                }
            }

            static List<Stock> Stocks = new List<Stock>
            {
                // new Stock {day=1, lowPrice=52,highPrice=54},
                // new Stock {day=2, lowPrice=47,highPrice=53},
                // new Stock {day=3, lowPrice=47,highPrice=53},                
                // new Stock {day=4, lowPrice=51,highPrice=60},
                // new Stock {day=5, lowPrice=51,highPrice=60}                
                //new Stock {day=6, lowPrice=45,highPrice=69}
                
                new Stock {day=1, lowPrice=53,highPrice=57},
                new Stock {day=2, lowPrice=52,highPrice=60},
                new Stock {day=3, lowPrice=49,highPrice=58},
                new Stock {day=4, lowPrice=50,highPrice=55},
                new Stock {day=5, lowPrice=51,highPrice=59}
                
            };
        }
    }
