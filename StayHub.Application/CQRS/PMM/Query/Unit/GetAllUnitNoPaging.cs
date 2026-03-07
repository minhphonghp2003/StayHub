using MediatR;
using Shared.Response;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Query.Unit
{
    public record GetAllUnitNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<UnitDTO>>>;
    public sealed class GetAllUnitNoPagingQueryHandler(IUnitRepository repository)
        : BaseResponseHandler, IRequestHandler<GetAllUnitNoPagingQuery, BaseResponse<List<UnitDTO>>>
    {
        public async Task<BaseResponse<List<UnitDTO>>> Handle(GetAllUnitNoPagingQuery request, CancellationToken ct)
        {
            var result = await repository.GetManyAsync(filter: e => e.UnitGroup.PropertyId == request.propertyId, selector: (e, i) => new UnitDTO
            {
                Id = e.Id,
                Name = e.Name
            });
            return Success(result.ToList());
        }
    }
}
