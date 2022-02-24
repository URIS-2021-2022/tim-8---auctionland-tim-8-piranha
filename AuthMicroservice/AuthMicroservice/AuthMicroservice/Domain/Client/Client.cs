using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthMicroservice.Domain
{
    /// <summary>
    /// Class that represents client entity.
    /// </summary>
    [Table("Clients")]
    public class Client : BaseEntity
    {
        /// <summary>
        /// First name of the client.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the client.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Username of the client.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password of the client.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Uid of the user type entity.
        /// </summary>
        [ForeignKey("UserType")]
        public string UserTypeUid { get; set; }

        /// <summary>
        /// Type of the user.
        /// </summary>
        [Required]
        public UserType UserType { get; set; }
    }
}
