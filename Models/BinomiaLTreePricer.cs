using PricingEngine.Greeks;
using PricingEngine.Instruments;
using PricingEngine.MarketData;

namespace PricingEngine.Models
{
    public class BinomialTreePricer : IOptionPricer
    {
        private readonly int _steps;

        public BinomialTreePricer(int steps = 500)
        {
            _steps = steps;
        }

        public double Price(Option option, MarketDataSnapshot data)
        {
            double S = data.Spot;
            double K = option.Strike;
            double T = option.TimeToMaturity;
            double r = data.RiskFreeRate;
            double sigma = data.Volatility;

            double dt = T / _steps;
            double u = Math.Exp(sigma * Math.Sqrt(dt));
            double d = 1.0 / u;
            double disc = Math.Exp(-r * dt);
            double p = (Math.Exp(r * dt) - d) / (u - d);

            double[] prices = new double[_steps + 1];

            // Terminal payoff
            for (int i = 0; i <= _steps; i++)
            {
                double ST = S * Math.Pow(u, _steps - i) * Math.Pow(d, i);

                prices[i] = option.Type == OptionType.Call
                    ? Math.Max(ST - K, 0)
                    : Math.Max(K - ST, 0);
            }

            // Backward induction
            for (int step = _steps - 1; step >= 0; step--)
            {
                for (int i = 0; i <= step; i++)
                {
                    prices[i] = disc * (p * prices[i] + (1 - p) * prices[i + 1]);
                }
            }

            return prices[0];
        }

        public GreeksResult CalculateGreeks(Option option, MarketDataSnapshot data)
        {
            // Tree Greeks usually computed via node sensitivities or finite differences
            return new GreeksResult();
        }
    }

}
