namespace LoanSystem.Application.Abstraction.Generator
{
    public interface IStringGenerator
    {
        string Generate8DigitRoutingNumber();
        string Generate9DigitAccountNumber();
        string Generate3DigitSecurityCode();
        string Generate16DigitNumber();
        bool CheckLuhn(string creditCardNumber);
    }
}
