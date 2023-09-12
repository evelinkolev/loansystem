namespace LoanSystem.Models.Exceptions
{
    public class NoCardException : Exception
    {
        public NoCardException() : base("Looks like you don't have any card. Please feel free to create one and then come back again to process your payment.")
        {

        }
    }
}
