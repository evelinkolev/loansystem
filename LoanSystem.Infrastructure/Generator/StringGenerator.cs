using LoanSystem.Application.Abstraction.Generator;
using System.Text;

namespace LoanSystem.Infrastructure.Generator
{
    public partial class StringGenerator : IStringGenerator
    {
        private readonly Random _random;

        public StringGenerator()
        {
            _random = new Random();
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
    }
}
