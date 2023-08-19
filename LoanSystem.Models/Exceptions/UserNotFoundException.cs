namespace LoanSystem.Models.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string? Email { get; }
        public Guid UserId { get; }

        public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
        {
            UserId = userId;
        }

        public UserNotFoundException(string email) : base($"User with email: '{email}' was not found.")
        {
            Email = email;
        }
    }
}
