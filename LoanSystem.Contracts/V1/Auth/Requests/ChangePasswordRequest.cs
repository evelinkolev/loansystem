namespace LoanSystem.Contracts.V1.Auth.Requests
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword);

}
