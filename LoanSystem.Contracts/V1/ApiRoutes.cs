﻿namespace LoanSystem.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Account
        {
            public const string Signup = Base + "/account/sign-up";
            public const string Signin = Base + "/account/sign-in";
        }

        public static class Password
        {
            public const string Change = Base + "/password/{userId}/change";
        }

        public static class Payers
        {
            public const string Create = Base + "/payers/user";
            public const string Update = Base + "/payers/user/{payerId}";
        }

        public static class Card
        {
            public const string Create = Base + "/card/payer";
            public const string Delete = Base + "/card/payer/{cardId}";
        }

        public static class Payments
        {
            public const string Create = Base + "/payments/payer";
        }
    }
}
