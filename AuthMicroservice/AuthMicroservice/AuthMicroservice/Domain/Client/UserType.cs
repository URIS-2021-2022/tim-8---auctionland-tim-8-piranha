namespace AuthMicroservice.Domain
{
    using Common.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserTypes")]
    public class UserType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
