using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications.CountWithSpec;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedDto<BrandDto>> GetAllAsync(SpecificationBrandAndTypeParameters specification)
        {
            var spec = new BrandSpecifications(specification);
            var specCount = new BrandCountWithSpec(specification);
            var result = await _unitOfWork.BrandReadRepository.GetAllSpecAsync(spec);
            var Count = await _unitOfWork.BrandReadRepository.GetCountWithSpecAsync(specCount);
            var resultDto = _mapper.Map<IReadOnlyList<BrandDto>>(result);
            var resultPaginated = new PaginatedDto<BrandDto>
            {
                Data = resultDto,
                PageIndex = specification.PageIndex,
                PageSize = specification.PageSize,
                TotalCount = Count
            };
            return resultPaginated;
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var spec = new BrandSpecifications(id);
            var result = await _unitOfWork.BrandReadRepository.GetByIdSpecAsync(spec);
            var resultDto = _mapper.Map<BrandDto>(result);
            return resultDto;
        }
    }
}
