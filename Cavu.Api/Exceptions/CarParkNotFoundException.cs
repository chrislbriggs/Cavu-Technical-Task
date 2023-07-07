namespace Cavu.Api.Exceptions
{
    public class CarParkNotFoundException : CustomException
    {
        public CarParkNotFoundException(string id) :base($"Car Park: {id} cannot be found.")
        {
            
        }
    }
}