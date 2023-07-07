namespace Cavu.Api.Dtos
{
    public class BookingRequestDto
    {
        public string CarParkId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}