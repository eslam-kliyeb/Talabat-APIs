using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<PaginatedDto<ProductDto>> GetAllAsync(SpecificationProductParameters specification);
        Task<ProductDto> GetByIdAsync(int id);
    }
}
