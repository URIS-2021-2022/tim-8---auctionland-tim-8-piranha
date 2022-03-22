﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    /// <summary>
    /// Represents individual documentation confirmation
    /// </summary>
    public class DocumentationIndividualConformation
    {
        #region
        /// <summary>
        /// Individual documentation ID
        /// </summary>
        public Guid DocumentationIndividualId { get; set; }
        /// <summary>
        /// Individual first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Individual surname
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Individual identification number
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// ID of auction that this documentation belongs to
        /// </summary>
        /// 
        [BindProperty]
        public Guid AuctionId { get; set; }

        public Auction Auction { get; set; }

        #endregion
    }
}
