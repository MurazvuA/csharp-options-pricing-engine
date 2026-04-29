namespace PricingEngine.Models
{
    public static class Normal
    {
        public static double Cdf(double x)
        {
            return 0.5 * (1.0 + Erf(x / Math.Sqrt(2.0)));
        }

        public static double Pdf(double x)
        {
            return Math.Exp(-0.5 * x * x) / Math.Sqrt(2.0 * Math.PI);
        }

        public static double Sample(Random rng)
        {
            double u1 = rng.NextDouble();
            double u2 = rng.NextDouble();
            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        }

        private static double Erf(double x)
        {
            // Abramowitz–Stegun approximation
            double t = 1.0 / (1.0 + 0.5 * Math.Abs(x));
            double tau = t * Math.Exp(
                -x * x - 1.26551223 +
                t * (1.00002368 +
                t * (0.37409196 +
                t * (0.09678418 +
                t * (-0.18628806 +
                t * (0.27886807 +
                t * (-1.13520398 +
                t * (1.48851587 +
                t * (-0.82215223 +
                t * 0.17087277)))))))));

            return x >= 0 ? 1 - tau : tau - 1;
        }
    }

}
