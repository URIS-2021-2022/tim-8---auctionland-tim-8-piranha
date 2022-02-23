namespace AuthMicroservice.Consts
{
    using System;

    public class RouteConsts
    {
        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_AUTH_BASE = ROUTE_API_BASE + "/auth";

        public const string ROUTE_AUTH_SIGN_IN = ROUTE_AUTH_BASE + "/sign-in";

        public const string ROUTE_AUTH_VALIDATE_TOKEN = ROUTE_AUTH_BASE + "/token-validation";
    }
}
