namespace LoanSystem.Models.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Reason { get; }
        public InvalidPasswordException(string reason) : base($"Invalid password: {reason}.")
        {
            Reason = reason;
        }
    }
}
