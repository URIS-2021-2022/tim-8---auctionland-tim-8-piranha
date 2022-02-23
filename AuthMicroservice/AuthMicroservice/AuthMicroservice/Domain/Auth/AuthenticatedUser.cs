namespace AuthMicroservice.Domain
{
    using Common.Domain;
    using System;

    public class AuthenticatedUser : BaseEntity
    {
        public string username { get; set; }

        public string role { get; set; }

        public AuthenticatedUser(string username, string role)
        {
            this.username = username;
            this.role = role;
        }
    }
}
