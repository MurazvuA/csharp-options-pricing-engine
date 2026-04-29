namespace PricingEngine.Instruments
{
    public class BarrierOption : Option
    {
        public double Barrier { get; set; }
        public bool IsUpAndOut { get; set; }
    }

}
