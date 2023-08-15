using LoanSystem.Application.Abstraction.Time;

namespace LoanSystem.Infrastructure.Time
{
    public partial class UtcClock : IClock
    {
        public DateTime CurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
}
