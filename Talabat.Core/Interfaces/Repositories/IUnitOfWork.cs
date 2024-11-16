using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Repositories.WriteRepository;

namespace Talabat.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IProductReadRepository ProductReadRepository { get; }
        public IBrandReadRepository BrandReadRepository { get; }
        public ITypeReadRepository TypeReadRepository { get; }
        public IDeliveryMethodReadRepository deliveryMethodReadRepository { get; }
        public IOrderWriteRepository OrderWriteRepository { get; }
        public IOrderReadRepository orderReadRepository { get; }
        Task<int> CompletesAsync();
    }
}
