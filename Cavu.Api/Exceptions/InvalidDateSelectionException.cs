namespace Cavu.Api.Exceptions
{
    public class InvalidDateSelectionException : CustomException
    {
        public InvalidDateSelectionException(DateTime dateFrom, DateTime dateTo, string reason)
        : base($"DateFrom: {dateFrom} and DateTo: {dateTo} are invalid selections. {reason}")
        {
        }
    }
}