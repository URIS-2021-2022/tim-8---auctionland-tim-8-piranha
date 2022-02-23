namespace AuthMicroservice.Domain
{
    using Common.Domain;
    using System;

    /// <summary>
    /// Authenticated user entity class.
    /// </summary>
    public class AuthenticatedUser : BaseEntity
    {
        /// <summary>
        /// Username (email) of the user.
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Name of the user role.
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// Constructor for the authenticated user entity.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="role">Role of the user.</param>
        public AuthenticatedUser(string username, string role)
        {
            this.username = username;
            this.role = role;
        }
    }
}
