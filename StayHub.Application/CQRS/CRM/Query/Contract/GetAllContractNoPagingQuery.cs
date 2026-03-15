using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.CRM.Query.Contract
{

    public record GetAllContractNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<ContractDTO>>>;
    public sealed class GetAllContractNoPagingQueryHandler(IContractRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllContractNoPagingQuery, BaseResponse<List<ContractDTO>>>
    {
        public async Task<BaseResponse<List<ContractDTO>>> Handle(GetAllContractNoPagingQuery request, CancellationToken ct)
        {
            var result = await repository.GetManyAsync(
                filter: x => x.Unit.UnitGroup.PropertyId == request.propertyId,
                include: e => e.Include(j => j.Unit),
                selector: (x, i) => new ContractDTO
                {
                    Id = x.Id,
                    UnitId = x.UnitId,
                    Unit = new DTO.PMM.UnitDTO
                    {
                        Id = x.Unit.Id,
                        Name = x.Unit.Name,
                    },
                }
            );
            return Success(result.ToList());
        }
    }
}
