namespace Cavu.Api.Dtos
{
    public class BookingResponseDto
    {
        public string Id { get; set; }

        public string CarParkId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}