using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.APIs.Controllers
{
    public class TypesController : ApiBaseController
    {
        private readonly ITypeService _typeService;

        public TypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }
        [Authorize]
        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<PaginatedDto<TypeDto>>> GetTypes([FromQuery] SpecificationBrandAndTypeParameters specificationBrandAndTypeParameters)
        {
            var result = await _typeService.GetAllAsync(specificationBrandAndTypeParameters);
            return Ok(result);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TypeDto), StatusCodes.Status200OK)]
        [HttpGet("GetTypeById/{id}")]
        public async Task<ActionResult<TypeDto>> GetType(int id)
        {
            var result =await _typeService.GetByIdAsync(id);
            if (result is null) return NotFound(new ApiResponse(404, "Type not found."));
            return Ok(result);
        }
    }
}
