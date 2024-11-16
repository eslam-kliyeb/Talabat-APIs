using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.APIs.Controllers
{
    public class BrandsController : ApiBaseController
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [Authorize]
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<PaginatedDto<BrandDto>>> GetBrands([FromQuery] SpecificationBrandAndTypeParameters specificationBrandAndTypeParameters)
        {
            var result = await _brandService.GetAllAsync(specificationBrandAndTypeParameters);
            return Ok(result);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
        [HttpGet("GetBrandById/{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var result = await _brandService.GetByIdAsync(id);
            if (result is null) return NotFound(new ApiResponse(404, "Brand not found."));
            return Ok(result);
        }
    }
}
