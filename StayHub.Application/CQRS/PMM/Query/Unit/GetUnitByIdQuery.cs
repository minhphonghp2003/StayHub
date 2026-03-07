using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;
namespace StayHub.Application.CQRS.PMM.Query.Unit;

public record GetUnitByIdQuery(int Id) : IRequest<BaseResponse<UnitDTO>>;
public sealed class GetUnitByIdQueryHandler(IUnitRepository repository, IHttpContextAccessor httpContextAccessor)
    : BaseResponseHandler, IRequestHandler<GetUnitByIdQuery, BaseResponse<UnitDTO>>
{
    public async Task<BaseResponse<UnitDTO>> Handle(GetUnitByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id && x.UnitGroup.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()), include: x => x.Include(j => j.UnitGroup), selector: (x) => new UnitDTO
        {
            Id = x.Id,
            Name = x.Name,
            Status = x.Status,
            BasePrice = x.BasePrice,
            MaximumCustomer = x.MaximumCustomer,
            IsActive = x.IsActive,
            UnitGroup = new UnitGroupDTO
            {
                Id = x.Id,
                Name = x.Name,

            }

        });
        return result == null ? Failure<UnitDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}