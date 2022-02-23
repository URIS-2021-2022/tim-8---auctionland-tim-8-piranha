using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.BoardNumber
{
    /// <summary>
    /// Board number confirmation DTO for communication with user
    /// </summary>
    public class BoardNumberConfirmationDto
    {
        /// <summary>
        /// Number - Broj table
        /// Example : 2
        /// </summary>
        public int number { get; set; }
    }
}
