using LoanSystem.Models.Domain.Base;

namespace LoanSystem.Models.Domain
{
    public partial class Payment : EntityBase
    {
        public decimal Amount { get; set; }
        public string RequestType { get; set; } = "ONE_TIME";
        public Guid PayerId { get; set; }
        public Payer? Payer { get; set; }
    }
}
