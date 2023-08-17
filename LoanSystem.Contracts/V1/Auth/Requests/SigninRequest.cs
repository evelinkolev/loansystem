namespace LoanSystem.Contracts.V1.Auth.Requests
{
    public record SigninRequest(
        string Email,
        string Password);
}
