namespace AuthMicroservice.Consts
{
    /// <summary>
    /// Route constants used in controllers.
    /// </summary>
    public class RouteConsts
    {
        /// <summary>
        /// Base route.
        /// </summary>
        public const string ROUTE_API_BASE = "/api";

        /// <summary>
        /// Auth resource route.
        /// </summary>
        public const string ROUTE_AUTH_BASE = ROUTE_API_BASE + "/auth";

        /// <summary>
        /// Sign in endpoint route.
        /// </summary>
        public const string ROUTE_AUTH_SIGN_IN = ROUTE_AUTH_BASE + "/sign-in";

        /// <summary>
        /// Token validation endpoint route.
        /// </summary>
        public const string ROUTE_AUTH_VALIDATE_TOKEN = ROUTE_AUTH_BASE + "/token-validation";
    }
}
