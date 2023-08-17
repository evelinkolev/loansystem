namespace LoanSystem.Models.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {           
        }
    }
}
