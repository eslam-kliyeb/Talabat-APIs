using AutoMapper;
using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Repositories;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _databaseRedis;
        private readonly IMapper _mapper; 
        public BasketRepository(IConnectionMultiplexer redis,IMapper mapper)
        {
            _databaseRedis = redis.GetDatabase();
            _mapper = mapper;
        }
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var Basket = await _databaseRedis.StringGetAsync(id);
            if (Basket.IsNull) return null;
            return  JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var Basket = JsonSerializer.Serialize(customerBasket);
            var result= await _databaseRedis.StringSetAsync(customerBasket.Id, Basket,TimeSpan.FromDays(1));
            if (!result) return null;
            return await GetBasketAsync(customerBasket.Id);
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _databaseRedis.KeyDeleteAsync(id);
        }
    }
}
