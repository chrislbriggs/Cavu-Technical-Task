using DataAccess.Enums;

namespace DataAccess.Models
{
    public class PricingModel
    {
        public PricingPeriod PricingPeriod { get; set; }

        public PricingSeason PricingSeason { get; set; }

        public decimal DailyRate { get; set; }
    }
}