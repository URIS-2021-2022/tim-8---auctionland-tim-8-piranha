using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Domain
{
    public class BaseEntity
    {
        [Key]
        public string Uid { get; set; } = Guid.NewGuid().ToString();
    }
}
