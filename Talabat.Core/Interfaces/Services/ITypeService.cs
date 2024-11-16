using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Core.Interfaces.Services
{
    public interface ITypeService
    {
        Task<PaginatedDto<TypeDto>> GetAllAsync(SpecificationBrandAndTypeParameters specification);
        Task<TypeDto> GetByIdAsync(int id);
    }
}
