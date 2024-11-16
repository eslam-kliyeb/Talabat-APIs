using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Errors;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications;

namespace Talabat.APIs.Controllers
{

    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderResDto), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderResDto>> CreateOrder(OrderDto order)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<Address>(order.ShippingAddress);
            var result = await _orderService.CreateOrderAsync(BuyerEmail,order.BasketId,order.DeliveryMethodId,MappedAddress);
            if (result is null) return BadRequest(new ApiResponse(400,"There is a Problem With Your Order"));
            return Ok(result);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IReadOnlyList<OrderResDto>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderResDto>>> GetOrdersForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _orderService.GetOrdersForSpecificUser(BuyerEmail);
            if (result is null) return NotFound(new ApiResponse(404, "There is no Orders For This User"));
            return Ok(result);
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(OrderResDto), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetOrderByIdForUser")]
        public async Task<ActionResult<OrderResDto>> GetOrderByIdForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _orderService.GetOrdersByIdForSpecificUser(BuyerEmail,id);
            if (result is null) return NotFound(new ApiResponse(404, $"There is no Orders With{id} For This User"));
            return Ok(result);
        }
        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<OrderResDto>>> GetDeliveryMethods()
        {
            var result =await  _unitOfWork.deliveryMethodReadRepository.GetAllSpecAsync(new DeliveryMethodSpecifications());
            return Ok(result);
        }

    }
}
