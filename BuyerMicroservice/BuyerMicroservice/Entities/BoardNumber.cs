using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
    /// <summary>
    /// Board Number model
    /// </summary>
    public class BoardNumber 
    {
        /// <summary>
        /// board Number ID - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        [Key]
        public Guid boardNumberID { get; set; }
        /// <summary>
        /// Number - Broj table
        /// Example : 2
        /// </summary>
        public int number { get; set; }

        
    }
}
