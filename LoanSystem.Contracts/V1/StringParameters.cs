namespace LoanSystem.Contracts.V1
{
    public record StringParameters(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder);
}
