namespace AuthMicroservice.Consts
{
    /// <summary>
    /// Route constants used in controllers.
    /// </summary>
    public static class RouteConsts
    {
        /// <summary>
        /// Base route.
        /// </summary>
        public const string ROUTE_API_BASE = "/api";

        #region Auth

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

        #endregion

        #region Client

        /// <summary>
        /// Base route for client resource.
        /// </summary>
        public const string ROUTE_CLIENT_BASE = ROUTE_API_BASE + "/clients";

        /// <summary>
        /// Route for a single user type.
        /// </summary>
        public const string ROUTE_CLIENT_GET_ONE_BY_UID = ROUTE_CLIENT_BASE + "/{uid}";

        #endregion

        #region User type

        /// <summary>
        /// Base route for user type resource.
        /// </summary>
        public const string ROUTE_USER_TYPE_BASE = ROUTE_API_BASE + "/user-types";

        /// <summary>
        /// Route for a single user type.
        /// </summary>
        public const string ROUTE_USER_TYPE_GET_ONE_BY_UID = ROUTE_USER_TYPE_BASE + "/{uid}";

        #endregion

    }
}
