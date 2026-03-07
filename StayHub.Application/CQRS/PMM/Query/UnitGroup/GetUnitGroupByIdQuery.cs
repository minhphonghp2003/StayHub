using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Response;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.PMM;
using System.Net;
namespace StayHub.Application.CQRS.PMM.Query.UnitGroup;
public record GetUnitGroupByIdQuery(int Id) : IRequest<BaseResponse<UnitGroupDTO>>;
public sealed class GetUnitGroupByIdQueryHandler(IUnitGroupRepository repository,IHttpContextAccessor httpContextAccessor) 
    : BaseResponseHandler, IRequestHandler<GetUnitGroupByIdQuery, BaseResponse<UnitGroupDTO>> 
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(GetUnitGroupByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id && x.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()), (x) => new UnitGroupDTO { Id = x.Id, Name = x.Name });
        return result == null ? Failure<UnitGroupDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}