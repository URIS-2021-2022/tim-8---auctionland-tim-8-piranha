﻿using BuyerMicroservice.Models.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.Individual
{
    public class IndividualUpdateDto : BuyerUpdateDto
    {
        public string surname { get; set; }

        public string JMBG { get; set; }
    }
}
