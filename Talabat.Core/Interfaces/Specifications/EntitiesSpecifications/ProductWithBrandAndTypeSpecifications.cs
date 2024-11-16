using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;
using Talabat.Core.Interfaces.Specifications.SpecificationsHelpers.Sorting;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(SpecificationProductParameters specs) 
              : base(p=>string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search))
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            ApplyPagination(specs.PageSize, specs.PageIndex);
            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case SortingProductParameters.NameAsc:
                        OrderBy = x => x.Name;
                        break;
                    case SortingProductParameters.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;
                    case SortingProductParameters.PriceAsc:
                        OrderBy = x => x.Price;
                        break;
                    case SortingProductParameters.PriceDesc:
                        OrderByDesc = x => x.Price;
                        break;
                    default:
                        OrderBy = x => x.Name;
                        break;
                }
            }
            else
            {
                OrderBy = x => x.Name;
            }
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
