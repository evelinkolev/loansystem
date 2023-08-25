namespace LoanSystem.Models.Exceptions
{
    public class BadRequestPayerException : Exception
    {
        public BadRequestPayerException() : base("An error occurred while verifying your request.")
        {
            
        }
    }
}
