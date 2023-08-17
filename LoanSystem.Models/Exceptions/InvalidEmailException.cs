namespace LoanSystem.Models.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public string Email { get; }
        public InvalidEmailException(string email) : base($"State is invalid: '{email}'.")
        {
            Email = email;
        }
    }
}
