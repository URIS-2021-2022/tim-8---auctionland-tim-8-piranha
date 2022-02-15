﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Models
{
    /// <summary>
    /// Represents Individual documentation model
    /// </summary>
    public class DocumentationIndividualDto
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

        public Guid AuctionId { get; set; }
        //public AuctionDto AuctionDto { get; set; }

        #endregion
    }
}
