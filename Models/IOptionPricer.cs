using PricingEngine.Greeks;
using PricingEngine.Instruments;
using PricingEngine.MarketData;

namespace PricingEngine.Models
{
    public interface IOptionPricer
    {
        double Price(Option option, MarketDataSnapshot data);
        GreeksResult CalculateGreeks(Option option, MarketDataSnapshot data);
    }
}
