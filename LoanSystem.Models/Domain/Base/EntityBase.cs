namespace LoanSystem.Models.Domain.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
