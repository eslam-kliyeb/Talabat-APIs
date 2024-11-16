﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications.CountWithSpec
{
    public class ProductCountWithSpec : BaseSpecifications<Product>
    {
        public ProductCountWithSpec(SpecificationProductParameters specs)
              : base(p => string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search))
        {

        }
    }
}