﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotConfirmation
    {
        public Guid PlotId { get; set; }

        public Guid PlotCultureId { get; set; }

        public Guid PlotCadastralMunicipalityId { get; set; }

        public Guid PlotWorkabilityId { get; set; }

        public int PlotSurfaceArea { get; set; }

        public string PlotNumber { get; set; }

        public string PlotRealEstateListNumber { get; set; }

        public string PlotCurrentCulture { get; set; }

        public string PlotCurrentWorkability { get; set; }
    }
}
