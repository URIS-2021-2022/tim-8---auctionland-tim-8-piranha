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

        /// <summary>
        /// Base route for client resource.
        /// </summary>
        public const string ROUTE_CLIENT_BASE = ROUTE_API_BASE + "/client";

        /// <summary>
        /// Route for getting a single client by uuid.
        /// </summary>
        public const string ROUTE_CLIENT_GET_ONE_BY_UID = ROUTE_CLIENT_BASE + "{uid}";
    }
}
