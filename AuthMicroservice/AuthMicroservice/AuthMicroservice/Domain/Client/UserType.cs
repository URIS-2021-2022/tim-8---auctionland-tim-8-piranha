namespace AuthMicroservice.Domain
{
    using Common.Domain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class that represents user type entity.
    /// </summary>
    [Table("UserTypes")]
    public class UserType : BaseEntity
    {
        /// <summary>
        /// Name of the user type.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
