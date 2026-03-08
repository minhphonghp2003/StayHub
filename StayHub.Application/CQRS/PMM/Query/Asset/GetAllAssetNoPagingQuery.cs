using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Query.Asset
{

    public record GetAllAssetNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<AssetDTO>>>;
    public sealed class GetAllAssetNoPagingQueryHandler(IAssetRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllAssetNoPagingQuery, BaseResponse<List<AssetDTO>>>
    {
        public async Task<BaseResponse<List<AssetDTO>>> Handle(GetAllAssetNoPagingQuery request, CancellationToken ct)
        {
            var result = await repository.GetManyAsync(
                filter: x => x.PropertyId == request.propertyId,
                include: x => x.Include(e => e.Type),
                selector: (x, i) => new AssetDTO
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
