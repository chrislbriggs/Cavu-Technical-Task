namespace Cavu.Api.Exceptions
{
    public class PriceUnavailableException : CustomException
    {
        public PriceUnavailableException()
            : base($"Pricing unavailable for selected dates.")
        {
        }
    }
}
