using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.SortingSpecifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<PaginatedDto<ProductDto>>> GetProducts([FromQuery] SpecificationProductParameters specificationParameters)
        {
            var result =await  _productService.GetAllAsync(specificationParameters);
            return Ok(result);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result is null) return NotFound(new ApiResponse(404, "Product not found."));
            return Ok(result);
        }
    }
}
