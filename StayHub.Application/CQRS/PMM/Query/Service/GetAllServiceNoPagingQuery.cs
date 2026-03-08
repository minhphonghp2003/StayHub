using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Query.Service
{

    public record GetAllServiceNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<ServiceDTO>>>;
    public sealed class GetAllServiceNoPagingQueryHandler(IServiceRepository repository ) : BaseResponseHandler, IRequestHandler<GetAllServiceNoPagingQuery, BaseResponse<List<ServiceDTO>>>
    {
        public async Task<BaseResponse<List<ServiceDTO>>> Handle(GetAllServiceNoPagingQuery request, CancellationToken ct)
        {
            var result = await repository.GetManyAsync(
                filter: x => x.PropertyId == request.propertyId,
                selector: (x, i) => new ServiceDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                }
            );
            return Success(result.ToList());
        }
    }
}
