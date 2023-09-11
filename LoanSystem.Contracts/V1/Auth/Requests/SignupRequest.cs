namespace LoanSystem.Contracts.V1.Auth.Requests
{
    public record SignupRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password);
}
