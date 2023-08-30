using LoanSystem.Application.Abstraction.Generator;
using System.Text;

namespace LoanSystem.Infrastructure.Generator
{
    public partial class StringGenerator : IStringGenerator
    {
        private static readonly Func<char, int> CharToInt = c => c - '0';
        private readonly Func<int, int> doubleDigit = i => (i * 2).ToString().ToCharArray().Select(CharToInt).Sum();
        private readonly Func<int, bool> isEven = i => i % 2 == 0;
        private readonly Random _random;

        public StringGenerator()
        {
            _random = new Random();
        }

        public bool CheckLuhn(string creditCardNumber)
        {
            var checkSum = creditCardNumber
                .ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray()
                .Select(CharToInt)
                .Reverse()
                .Select((digit, index) => isEven(index + 1) ? doubleDigit(digit) : digit)
                .Sum();

            return checkSum % 10 == 0;
        }

        public string Generate3DigitSecurityCode()
        {
            var stringBuilder = new StringBuilder();
            while (stringBuilder.Length < 3)
            {
                stringBuilder.Append(_random.Next(10).ToString());
            }

            return stringBuilder.ToString();
        }

        public string Generate8DigitRoutingNumber()
        {
            var stringBuilder = new StringBuilder();
            while (stringBuilder.Length < 8)
            {
                stringBuilder.Append(_random.Next(10).ToString());
            }

            return stringBuilder.ToString();
        }

        public string Generate9DigitAccountNumber()
        {
            var stringBuilder = new StringBuilder();
            while (stringBuilder.Length < 9)
            {
                stringBuilder.Append(_random.Next(10).ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
