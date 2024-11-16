using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Core.Interfaces.Services
{
    public interface IBrandService
    {
        Task<PaginatedDto<BrandDto>> GetAllAsync(SpecificationBrandAndTypeParameters specification);
        Task<BrandDto> GetByIdAsync(int id);
    }
}
