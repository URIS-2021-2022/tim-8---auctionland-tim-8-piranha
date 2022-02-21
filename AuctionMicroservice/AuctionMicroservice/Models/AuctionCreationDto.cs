using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    /// <summary>
    /// Represents auction creation model
    /// </summary>
    public class AuctionCreationDto
    {
        #region


        /// <summary>
        /// Auction number
        /// </summary>
        [Required]
        public int AuctionNum { get; set; }
        /// <summary>
        /// Year when auction is happening
        /// </summary>
        [Required]
        public int Year { get; set; }
        /// <summary>
        /// Date od auction
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// Auction restriction number
        /// </summary>
        [Required]
        public int Restriction { get; set; }
        /// <summary>
        /// Auction price step
        /// </summary>
        [Required]
        public int PriceStep { get; set; }


        public List<Guid> PublicBiddings { get; set; }
        /// <summary>
        /// Date of application deadline
        /// </summary>
        [Required]
        public DateTime ApplicationDeadline { get; set; }

        #endregion
    }
}
