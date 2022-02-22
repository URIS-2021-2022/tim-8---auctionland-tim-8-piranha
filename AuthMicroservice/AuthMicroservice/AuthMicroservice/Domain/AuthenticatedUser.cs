namespace AuthMicroservice.Domain
{
    using System;

    public class AuthenticatedUser : BaseEntity
    {
        private string email { get; set; }

        private string role { get; set; }
    }
}
