using PricingEngine.Instruments;
using PricingEngine.MarketData;

namespace PricingEngine.API
{
    public class PricingRequest
    {
        public string Model { get; set; }
        public EuropeanOption Option { get; set; }
        public MarketDataSnapshot MarketDataSnapshot { get; set; }
    }
}
