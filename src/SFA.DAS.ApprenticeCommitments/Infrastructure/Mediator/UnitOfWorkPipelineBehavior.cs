using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Infrastructure.MediatorExtensions;
using SFA.DAS.UnitOfWork.Managers;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator
{
    public class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkPipelineBehavior(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!(request is ITransactionCommand))
            {
                return await next();
            }

            await _unitOfWorkManager.BeginAsync();

            try
            {
                var response = await next();
                await _unitOfWorkManager.EndAsync();
                return response;
            }
            catch (Exception e)
            {
                await _unitOfWorkManager.EndAsync(e);
                throw;
            }
        }
    }
}