namespace LoanSystem.Models.Exceptions
{
    public class SinUpDisabledException : Exception
    {
        public SinUpDisabledException() : base("Sign up is disabled.")
        {            
        }
    }
}
