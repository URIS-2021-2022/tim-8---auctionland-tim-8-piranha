﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities
{
<<<<<<< HEAD
    /// <summary>
    /// Board Number confirmation model
    /// </summary>
    public class BoardNumberConfirmation
    {
        /// <summary>
        /// board Number ID - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        public Guid boardNumberID { get; set; }
        /// <summary>
        /// Number - Broj table
        /// Example : 2
        /// </summary>
=======
    public class BoardNumberConfirmation
    {
        public Guid boardNumberID { get; set; }
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public int number { get; set; }
    }
}
