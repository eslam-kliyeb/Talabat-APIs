using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;
using Talabat.Core.Interfaces.Specifications.SpecificationsHelpers.Sorting;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications
{
    public class TypeSpecifications : BaseSpecifications<ProductType>
    {
        public TypeSpecifications(SpecificationBrandAndTypeParameters specs) 
              : base(b => string.IsNullOrWhiteSpace(specs.Search) || b.Name.ToLower().Contains(specs.Search)) 
        {
            ApplyPagination(specs.PageSize, specs.PageIndex);
            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case SortingBrandAndTypeParameters.NameAsc:
                        OrderBy = x => x.Name;
                        break;
                    case SortingBrandAndTypeParameters.NameDesc:
                        OrderByDesc = x => x.Name;
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
        public TypeSpecifications(int id) : base(p => p.Id == id) { }
    }
}
