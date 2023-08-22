using LoanSystem.Models.Domain.Base;

namespace LoanSystem.Models.Domain
{
    public partial class Payer : EntityBase
    {
        public string? FullName { get; set; }
        public decimal Deposit { get; set; }
        public string? RoutingNumber { get; set; }
        public string? AccountNumber { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
