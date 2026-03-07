using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Query.UnitGroup
{
    public record GetAllUnitGroupNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<UnitGroupDTO>>>;
    public sealed class GetAllUnitGroupNoPagingQueryHandler(IUnitGroupRepository repository )
        : BaseResponseHandler, IRequestHandler<GetAllUnitGroupNoPagingQuery, BaseResponse<List<UnitGroupDTO>>>
    {
        public async Task<BaseResponse<List<UnitGroupDTO>>> Handle(GetAllUnitGroupNoPagingQuery request, CancellationToken ct)
        {
            var result = await repository.GetManyAsync(filter: e => e.PropertyId == request.propertyId, selector: (e, i) => new UnitGroupDTO
            {
                Id = e.Id,
                Name = e.Name
            });
            return Success(result.ToList());
        }
    }
}
