using MediatR;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Repositories.WriteRepository;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories.ReadRepository;
using Talabat.Repository.Repositories.WriteRepository;

namespace Talabat.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IProductReadRepository> _productReadRepository;
        private Lazy<IBrandReadRepository>   _brandReadRepository ;
        private Lazy<ITypeReadRepository>    _typeReadRepository ;
        private Lazy<IDeliveryMethodReadRepository> _deliveryMethodReadRepository;
        private Lazy<IOrderWriteRepository> _orderWriteRepository;
        private Lazy<IOrderReadRepository> _orderReadRepository;
        private ApplicationDbContext _dataContext;

        public UnitOfWork(ApplicationDbContext dataContext,IMediator mediator)
        {   
            _dataContext = dataContext;
            _productReadRepository = new Lazy<IProductReadRepository>(() => new ProductReadRepository(dataContext, mediator));
            _brandReadRepository = new Lazy<IBrandReadRepository>(() => new BrandReadRepository(dataContext, mediator));
            _typeReadRepository = new Lazy<ITypeReadRepository>(() => new TypeReadRepository(dataContext, mediator));
            _deliveryMethodReadRepository = new Lazy<IDeliveryMethodReadRepository>(() => new DeliveryMethodReadRepository(dataContext, mediator));
            _orderWriteRepository = new Lazy<IOrderWriteRepository>(() => new OrderWriteRepository(dataContext,mediator));
            _orderReadRepository = new Lazy<IOrderReadRepository>(() => new OrderReadRepository(dataContext, mediator));
        }
        public IProductReadRepository ProductReadRepository => _productReadRepository.Value;
        public IBrandReadRepository BrandReadRepository => _brandReadRepository.Value;
        public ITypeReadRepository TypeReadRepository => _typeReadRepository.Value;
        public IDeliveryMethodReadRepository deliveryMethodReadRepository => _deliveryMethodReadRepository.Value;
        public IOrderWriteRepository OrderWriteRepository => _orderWriteRepository.Value;
        public IOrderReadRepository orderReadRepository => _orderReadRepository.Value;

        public async Task<int> CompletesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await _dataContext.DisposeAsync();
        }
    }
}
