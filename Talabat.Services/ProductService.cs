using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications.CountWithSpec;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedDto<ProductDto>> GetAllAsync(SpecificationProductParameters specification)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(specification);
            var specCount = new ProductCountWithSpec(specification);
            var result = await _unitOfWork.ProductReadRepository.GetAllSpecAsync(spec);
            var Count = await _unitOfWork.ProductReadRepository.GetCountWithSpecAsync(specCount);
            var resultDto = _mapper.Map<IReadOnlyList<ProductDto>>(result);
            var resultPaginated = new PaginatedDto<ProductDto>
            {
                Data = resultDto,
                PageIndex = specification.PageIndex,
                PageSize = specification.PageSize,
                TotalCount = Count
            };
            return resultPaginated;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var result = await _unitOfWork.ProductReadRepository.GetByIdSpecAsync(spec);
            var resultDto = _mapper.Map<ProductDto>(result);
            return resultDto;
        }
    }
}
