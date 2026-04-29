using System.Collections.Concurrent;
using PricingEngine.Greeks;
using PricingEngine.Instruments;
using PricingEngine.Models;
using PricingEngine.MarketData;

public class MonteCarloPricer : IOptionPricer
{
    private readonly int _paths;
    public MonteCarloPricer(int paths = 200_000)
    {
        _paths = paths;
    }
    public double Price(Option option, MarketDataSnapshot data)
    {
        double S0 = data.Spot;
        double K = option.Strike;
        double T = option.TimeToMaturity;
        double r = data.RiskFreeRate;
        double sigma = data.Volatility;

        double discount = Math.Exp(-r * T);
        double payoffSum = 0.0;
        object locker = new();

        Parallel.For(
            0,
            _paths,
            () => new Random(Guid.NewGuid().GetHashCode()),
            (i, state, rng) =>
            {
                double z = Normal.Sample(rng);
                double ST = S0 * Math.Exp(
                    (r - 0.5 * sigma * sigma) * T +
                    sigma * Math.Sqrt(T) * z);

                double payoff = option.Type == OptionType.Call
                    ? Math.Max(ST - K, 0)
                    : Math.Max(K - ST, 0);

                lock (locker)
                {
                    payoffSum += payoff;
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