using System.Security.Cryptography.X509Certificates;

namespace MyEvent
{
    // There reasons to use events when use delegate
    // 1. Replace other subscribers by reassigning PriceChanged (instead of using the += operator)
    // 2. Clear all subscribers (by setting PriceChanged to null)
    // 3. Broadcast to other subscribers by invoking the delegate
    // whill be less robust insomuch

    //-----------------Dividing line-----------------
    // Delegate definition 
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
    public class Broadcaster
    {
        // Event declaration
        public event PriceChangedHandler PriceChanged;
        // Code in Broadcaster class has full access to PriceChanged
        // Outside of Broadcaster class can just use += or -= on the PriceChanged
    }
    //-----------------Dividing line-----------------


    public class Stock
    {
        string symbol;
        decimal price;

        public Stock(String symbol)
            => this.symbol = symbol;
        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        public decimal Price
        {
            get => price;
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }
        public virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
        static void stock_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                Console.WriteLine("Alert, 10% stock price increase!");
        }
        public static void StockMethod()
        {
            Stock stock = new Stock("THPW");
            stock.Price = 27.10M;
            stock.PriceChanged += stock_PriceChanged;
            stock.Price = 31.59M;
        }
    }

    public class PriceChangedEventArgs : System.EventArgs
    {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;

        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            this.LastPrice = lastPrice;
            this.NewPrice = newPrice;
        }
    }
    //-----------------Dividing line-----------------
}