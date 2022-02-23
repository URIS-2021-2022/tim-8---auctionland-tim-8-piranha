using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class AuctionDto
    {
        /// <summary>
<<<<<<< HEAD
=======
        /// Auction ID
        /// </summary>
        //public Guid AuctionId { get; set; }
        /// <summary>
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        /// Auction number
        /// </summary>
        public int AuctionNum { get; set; }
        /// <summary>
        /// Year when auction is happening
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Date od auction
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Auction restriction number
        /// </summary>
        public int Restriction { get; set; }
        /// <summary>
        /// Auction price step
        /// </summary>
        public int PriceStep { get; set; }
<<<<<<< HEAD
   
=======

        /// <summary>
        /// List of public biddings
        /// </summary>
        //public List<PublicBiddingDto> publicBiddings { get; set; }
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a

        /// <summary>
        /// Date of application deadline
        /// </summary>
<<<<<<< HEAD
        
=======
        /// 
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public DateTime ApplicationDeadline { get; set; }
    }
}
