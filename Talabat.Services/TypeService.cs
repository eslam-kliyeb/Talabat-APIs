using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications.CountWithSpec;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.Services
{
    public class TypeService : ITypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedDto<TypeDto>> GetAllAsync(SpecificationBrandAndTypeParameters specification)
        {
            var spec = new TypeSpecifications(specification);
            var specCount = new TypeCountWithSpec(specification);
            var result = await _unitOfWork.TypeReadRepository.GetAllSpecAsync(spec);
            var Count = await _unitOfWork.TypeReadRepository.GetCountWithSpecAsync(specCount);
            var resultDto = _mapper.Map<IReadOnlyList<TypeDto>>(result);
            var resultPaginated = new PaginatedDto<TypeDto>
            {
                Data = resultDto,
                PageIndex = specification.PageIndex,
                PageSize = specification.PageSize,
                TotalCount = Count
            };
            return resultPaginated;
        }

        public async Task<TypeDto> GetByIdAsync(int id)
        {
            var spec = new TypeSpecifications(id);
            var result = await _unitOfWork.TypeReadRepository.GetByIdSpecAsync(spec);
            var resultDto = _mapper.Map<TypeDto>(result);
            return resultDto;
        }
    }
}
