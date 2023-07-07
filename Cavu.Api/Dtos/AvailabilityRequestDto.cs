namespace Cavu.Api.Dtos
{
    public class AvailabilityRequestDto
    {
        public string CarParkId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}