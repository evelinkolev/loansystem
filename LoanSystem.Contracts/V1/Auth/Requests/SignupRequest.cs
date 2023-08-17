namespace LoanSystem.Contracts.V1.Auth.Requests
{
    public record SignupRequest(
        string FirstName,
        string Lastname,
        string Email,
        string Password);
}
