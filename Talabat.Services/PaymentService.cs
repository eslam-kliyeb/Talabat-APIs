using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Bcpg;
using Stripe;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Core.Interfaces.Specifications.EntitiesSpecifications;

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;


        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork, IProductService productService)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentId(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket == null) return null;
            //Amount = SubTotal + DM.Cost
            var ShippingPrice = 0M;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.deliveryMethodReadRepository.GetByIdSpecAsync(new DeliveryMethodSpecifications(Basket.DeliveryMethodId.Value));
                ShippingPrice = DeliveryMethod.Cost;
            }
            if (Basket.Item.Count > 0)
            {
                foreach (var Item in Basket.Item)
                {
                    var Product = await _productService.GetByIdAsync(Item.Id);
                    if(Item.Price!=Product.Price) Item.Price = Product.Price;
                }
            }
            var SubTotal = Basket.Item.Sum(Item => Item.Price * Item.Quantity);
            var Service = new PaymentIntentService();
            PaymentIntent paymentIntent = new PaymentIntent();
            if (string.IsNullOrEmpty(Basket.PaymentIntentId)) //Create 
            {
                var Options = new PaymentIntentCreateOptions()
                { 
                   Amount = (long)SubTotal*100+(long)ShippingPrice*100,
                   Currency = "usd",
                   PaymentMethodTypes = new List<string>() { "card"}
                };
                paymentIntent = await Service.CreateAsync(Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100,
                };
                paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId,Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string PaymentIntentId, bool flag)
        {
            var Order = await _unitOfWork.orderReadRepository.GetByIdSpecAsync(new OrderWithPaymentIntentIdSpecifications(PaymentIntentId));
            if (flag) Order.Status=OrderStatus.PaymentReceived;
            else Order.Status = OrderStatus.PaymentFailed;
            await _unitOfWork.OrderWriteRepository.Update(Order);
            return Order;
        }
    }
}
