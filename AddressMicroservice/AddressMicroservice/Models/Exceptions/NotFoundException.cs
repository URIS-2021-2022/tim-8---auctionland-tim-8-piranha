﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Models.Exceptions
{
    public class NotFoundException : Exception {
        public NotFoundException(string name, object key) : base($"Entity '{name}' ({key}) was not found.")
        {
        }
    }
}
