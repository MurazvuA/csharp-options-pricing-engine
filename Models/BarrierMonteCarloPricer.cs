using PricingEngine.Greeks;
using PricingEngine.Instruments;
using PricingEngine.MarketData;

namespace PricingEngine.Models
{
    public class BarrierMonteCarloPricer : IOptionPricer
    {
        private readonly int _paths;
        private readonly int _timeSteps;

        public BarrierMonteCarloPricer(int paths = 100_000, int timeSteps = 252)
        {
            _paths = paths;
            _timeSteps = timeSteps;
        }

        public double Price(Option option, MarketDataSnapshot data)
        {
            var barrierOption = option as BarrierOption;
            if (barrierOption == null)
                throw new ArgumentException("Invalid option type for barrier pricer");

            double S0 = data.Spot;
            double K = option.Strike;
            double B = barrierOption.Barrier;
            double T = option.TimeToMaturity;
            double r = data.RiskFreeRate;
            double sigma = data.Volatility;

            double dt = T / _timeSteps;
            double discount = Math.Exp(-r * T);

            double payoffSum = 0.0;
            object locker = new();

            Parallel.For(0, _paths, () => new Random(Guid.NewGuid().GetHashCode()),
                (i, state, rng) =>
                {
                    double S = S0;
                    bool knockedOut = false;

                    for (int t = 0; t < _timeSteps; t++)
                    {
                        double z = Normal.Sample(rng);
                        S *= Math.Exp(
                            (r - 0.5 * sigma * sigma) * dt +
                            sigma * Math.Sqrt(dt) * z);

                        if (barrierOption.IsUpAndOut && S >= B)
                        {
                            knockedOut = true;
                            break;
                        }
                    }

                    if (!knockedOut)
                    {
                        double payoff = option.Type == OptionType.Call
                            ? Math.Max(S - K, 0)
                            : Math.Max(K - S, 0);

                        lock (locker)
                        {
                            payoffSum += payoff;
                        }
                    }

                    return rng;
                },
                _ => { });

            return discount * payoffSum / _paths;
        }

        public GreeksResult CalculateGreeks(Option option, MarketDataSnapshot data)
        {
            return new GreeksResult();
        }
    }

}
