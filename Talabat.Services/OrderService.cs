using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        public OrderService(IBasketRepository basketRepository
                          ,IProductService productService
                          ,IUnitOfWork unitOfWork
                          ,IMapper mapper
                          ,IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _productService = productService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderResDto?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var Basket =await _basketRepository.GetBasketAsync(BasketId);
            var OrderItems = new List<OrderItem>();
            if (Basket?.Item.Count > 0)
            {
                foreach(var Item in Basket.Item)
                {
                    var Product =await _productService.GetByIdAsync(Item.Id);
                    var ProductItemOrder = new ProductItemOrder(Item.Id,Product.Name,Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrder,Product.Price,Item.Quantity);
                    OrderItems.Add(OrderItem);
                }
            }
            var SubTotal = OrderItems.Sum(Item=>Item.Price*Item.Quantity);
            var DeliveryMethod = await _unitOfWork.deliveryMethodReadRepository.GetByIdSpecAsync(new DeliveryMethodSpecifications(DeliveryMethodId));
            var ExOrder = await _unitOfWork.orderReadRepository.GetByIdSpecAsync(new OrderWithPaymentIntentIdSpecifications(Basket.PaymentIntentId));
            if (ExOrder is not null) 
            {
                await _unitOfWork.OrderWriteRepository.Delete(ExOrder);
                await _paymentService.CreateOrUpdatePaymentIntentId(BasketId);

            }
            var Order = new Order(BuyerEmail,ShippingAddress,DeliveryMethod,OrderItems,SubTotal,Basket.PaymentIntentId);
            var result = await _unitOfWork.OrderWriteRepository.AddAsync(Order);
            var resultDto = _mapper.Map<OrderResDto>(result);
            return resultDto is not null ? resultDto : null;
        }

        public async Task<OrderResDto> GetOrdersByIdForSpecificUser(string BuyerEmail, int OrderId)
        {
            var result =await _unitOfWork.orderReadRepository.GetByIdSpecAsync(new OrderSpecifications(BuyerEmail, OrderId));
            var resultDto = _mapper.Map<OrderResDto>(result);
            return (resultDto is not null ? resultDto : null)!;
        }

        public async Task<IReadOnlyList<OrderResDto>> GetOrdersForSpecificUser(string BuyerEmail)
        {
            var result = await _unitOfWork.orderReadRepository.GetAllSpecAsync(new OrderSpecifications(BuyerEmail));
            var resultDto = _mapper.Map<IReadOnlyList<OrderResDto>>(result);
            return (resultDto is not null ? resultDto : null)!;
        }
    }
}
