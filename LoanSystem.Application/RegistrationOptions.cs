namespace LoanSystem.Application
{
    public class RegistrationOptions
    {
        public bool Enabled { get; set; }
        public IEnumerable<string>? InvalidEmailProviders { get; set; }
    }
}
