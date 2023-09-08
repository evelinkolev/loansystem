namespace LoanSystem.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Accounts
        {
            public const string Signup = Base + "/accounts/sign-up";
            public const string Signin = Base + "/accounts/sign-in";
        }

        public static class Passwords
        {
            public const string Change = Base + "/passwords/{userId}/change";
        }

        public static class Payers
        {
            public const string Create = Base + "/payers";
            public const string Update = Base + "/payers/{payerId}";
        }

        public static class Cards
        {
            public const string Create = Base + "/cards";
            public const string Delete = Base + "/cards/{cardId}";
        }

        public static class Payments
        {
            public const string Create = Base + "/payments";
            public const string Get = Base + "/payments/{paymentId}";
        }
    }
}
