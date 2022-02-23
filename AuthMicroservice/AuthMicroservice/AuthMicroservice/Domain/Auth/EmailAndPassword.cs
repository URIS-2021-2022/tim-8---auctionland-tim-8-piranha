namespace AuthResource.Domain
{
    /// <summary>
    /// Email and password representation of the user.
    /// </summary>
    public class EmailAndPassword
    {
        /// <summary>
        /// Email address.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string password { get; set; }
    }
}
