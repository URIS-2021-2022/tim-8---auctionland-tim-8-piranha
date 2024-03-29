﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Data
{
    public interface ITypeRepository
    {
        Task<List<Entities.Type>> GetAllTypes();

        Task<Entities.Type> GetTypeById(Guid typeId);
    }
}
