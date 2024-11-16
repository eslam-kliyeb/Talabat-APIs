using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Repositories;

namespace Talabat.APIs.Controllers
{
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [Authorize]
        [HttpGet("GetCustomerBasket/{id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket == null)
                return Ok(new CustomerBasket(BasketId));

            return Ok(basket);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [HttpPost("UpdateBasket")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket([FromBody]CustomerBasket Basket)
        {
            var updatedBasket = await _basketRepository.UpdateBasketAsync(Basket);
            if (updatedBasket == null)
                return BadRequest(new ApiResponse(400, "Failed to update the basket."));
            return Ok(updatedBasket);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete("DeleteBasket")]
        public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(BasketId);
            if (!deleted)
                return NotFound(new ApiResponse(404, "Basket not found."));

            return NoContent();
        }
    }
}
