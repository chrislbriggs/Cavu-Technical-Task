namespace Cavu.Api.Exceptions
{
    public class BookingNotFoundException : CustomException
    {
        public BookingNotFoundException(string id) 
            : base($"Booking: {id} cannot be found.")
        {

        }
    }
}