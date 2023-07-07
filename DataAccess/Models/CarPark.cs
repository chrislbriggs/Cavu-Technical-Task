namespace DataAccess.Models
{
    public class CarPark : ModelBase
    {
        public CarPark()
        {
            PartitionKey = nameof(CarPark);
        }

        public string Description { get; set; }

        public int MaxAvailableSpaces { get; set; }

        public List<PricingModel> PricingModels { get; set; }
    }
}