using PricingEngine.Greeks;
using PricingEngine.Instruments;
using PricingEngine.MarketData;

namespace PricingEngine.Models
{
    public class BlackScholesPricer : IOptionPricer
    {
        public double Price(Option option, MarketDataSnapshot data)
        {
            double S = data.Spot;
            double K = option.Strike;
            double T = option.TimeToMaturity;
            double r = data.RiskFreeRate;
            double sigma = data.Volatility;

            double d1 = (Math.Log(S / K) + (r + 0.5 * sigma * sigma) * T)
                        / (sigma * Math.Sqrt(T));
            double d2 = d1 - sigma * Math.Sqrt(T);

            if (option.Type == OptionType.Call)
                return S * Normal.Cdf(d1) - K * Math.Exp(-r * T) * Normal.Cdf(d2);

            return K * Math.Exp(-r * T) * Normal.Cdf(-d2) - S * Normal.Cdf(-d1);
        }

        public GreeksResult CalculateGreeks(Option option, MarketDataSnapshot data)
        {
            double S = data.Spot;
            double K = option.Strike;
            double T = option.TimeToMaturity;
            double sigma = data.Volatility;

            double d1 = (Math.Log(S / K) + 0.5 * sigma * sigma * T)
                        / (sigma * Math.Sqrt(T));

            double pdf = Normal.Pdf(d1);

            return new GreeksResult
            {
                Delta = option.Type == OptionType.Call
                    ? Normal.Cdf(d1)
                    : Normal.Cdf(d1) - 1,
                Gamma = pdf / (S * sigma * Math.Sqrt(T)),
                Vega = S * pdf * Math.Sqrt(T),
                Theta = -S * pdf * sigma / (2 * Math.Sqrt(T))
            };
        }
    }

}
