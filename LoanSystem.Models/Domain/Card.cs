using LoanSystem.Models.Domain.Base;

namespace LoanSystem.Models.Domain
{
    public partial class Card : EntityBase
    {
        public string? Number { get; set; }
        public string? HolderName { get; set; }
        public string? ExpiryDate { get; set; }
        public string? SecurityCode { get; set; }
        public Guid PayerId { get; set; }
        public Payer? Payer { get; set; }
    }
}
