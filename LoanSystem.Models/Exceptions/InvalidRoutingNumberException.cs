namespace LoanSystem.Models.Exceptions
{
    public class InvalidRoutingNumberException : Exception
    {
        public string? RoutingNumber { get; }

        public InvalidRoutingNumberException(string routingNumber) : base($"Invalid routing number: '{routingNumber}'.")
        {
            RoutingNumber = routingNumber;
        }
    }
}
