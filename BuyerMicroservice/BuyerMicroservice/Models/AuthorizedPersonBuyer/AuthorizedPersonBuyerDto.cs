using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.AuthorizedPersonBuyer
{
<<<<<<< HEAD
    /// <summary>
    /// AuthorizedPersonBuyerDto-Auxiliary table for connecting more authorized persons with the buyer
    /// </summary>
    public class AuthorizedPersonBuyerDto
    {
        /// <summary>
        /// Authorized person ID - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid authorizedPersonId { get; set; }

        /// <summary>
        /// Buyer ID - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
=======
    public class AuthorizedPersonBuyerDto
    {
        public Guid authorizedPersonId { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public Guid buyerId { get; set; }
    }
}
