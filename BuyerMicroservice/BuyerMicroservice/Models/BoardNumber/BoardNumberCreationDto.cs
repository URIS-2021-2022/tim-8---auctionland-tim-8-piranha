﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Models.BoardNumber
{
    public class BoardNumberCreationDto
    {
        public Guid boardNumberID { get; set; }
        public int number { get; set; }
    }
}
