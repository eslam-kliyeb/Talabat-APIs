using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Specifications.Interface;
using Talabat.Repository.CQRS.DeliveryMethodRepository.Queries;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories.ReadRepository
{
    public class DeliveryMethodReadRepository : IDeliveryMethodReadRepository
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dataContext;
        public DeliveryMethodReadRepository(ApplicationDbContext dataContext, IMediator mediator)
        {
            _mediator = mediator;
            _dataContext = dataContext;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetAllSpecAsync(ISpecifications<DeliveryMethod> Spec)
        {
            var result = await _mediator.Send(new DeliveryMethodReadRepositoryQuery(_dataContext.DeliveryMethods, Spec));
            return (await result.ToListAsync())!;
        }

        public async Task<DeliveryMethod> GetByIdSpecAsync(ISpecifications<DeliveryMethod> Spec)
        {
            var result = await _mediator.Send(new DeliveryMethodReadRepositoryQuery(_dataContext.DeliveryMethods, Spec));
            return (await result.FirstOrDefaultAsync())!;
        }

        public async Task<int> GetCountWithSpecAsync(ISpecifications<DeliveryMethod> Spec)
        {
            var result = await _mediator.Send(new DeliveryMethodReadRepositoryQuery(_dataContext.DeliveryMethods, Spec));
            return (await result.CountAsync())!;
        }
    }
}
