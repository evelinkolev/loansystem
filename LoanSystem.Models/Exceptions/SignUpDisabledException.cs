namespace LoanSystem.Models.Exceptions
{
    public class SignUpDisabledException : Exception
    {
        public SignUpDisabledException() : base("Sign up is disabled.")
        {            
        }
    }
}
