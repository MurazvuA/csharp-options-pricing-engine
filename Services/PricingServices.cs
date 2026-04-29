using PricingEngine.Instruments;
using PricingEngine.MarketData;
using PricingEngine.Models;

namespace PricingEngine.Services
{
    public class PricingService
    {
        private readonly Dictionary<string, IOptionPricer> _pricers =
            new()
            {
            { "BlackScholes", new BlackScholesPricer() },
            { "MonteCarlo", new MonteCarloPricer() },
            { "Binomial", new BinomialTreePricer() },
            { "BarrierMonteCarlo", new BarrierMonteCarloPricer() }

            };

        public double Price(string model, Option option, MarketDataSnapshot data)
        {
            return _pricers[model].Price(option, data);
        }
    }

}
