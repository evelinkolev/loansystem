namespace LoanSystem.Contracts.V1.Auth.Responses
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
