namespace DataAccess.Models
{
    public class Booking : ModelBase
    {
        public Booking()
        {
            PartitionKey = nameof(Booking);
        }

        public string CarParkId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}