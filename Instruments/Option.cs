namespace PricingEngine.Instruments
{
    public abstract class Option
    {
        public double Strike { get; set; }
        public double TimeToMaturity { get; set; }
        public OptionType Type { get; set; }
    }

}
