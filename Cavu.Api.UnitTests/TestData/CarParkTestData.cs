using DataAccess.Enums;
using DataAccess.Models;

namespace Cavu.Api.UnitTests.TestData
{
    internal static class CarParkTestData
    {
        internal static CarPark CarPark => new CarPark
        {
            SortKey = "4783dd09-6366-4f8e-a182-1e96cfa90d41",
            Description = "Manchester Airport",
            MaxAvailableSpaces = 10,
            PricingModels = new List<PricingModel>
            {
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Summer,
                    DailyRate = 20,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Winter,
                    DailyRate = 10,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Summer,
                    DailyRate = 25,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Winter,
                    DailyRate = 15,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Spring,
                    DailyRate = 20,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Spring,
                    DailyRate = 18,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Autumn,
                    DailyRate = 20,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Autumn,
                    DailyRate = 18,
                }
            }
        };
    }
}