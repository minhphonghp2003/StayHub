using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.UnitGroup;
public record GetUnitGroupByIdQuery(int Id) : IRequest<BaseResponse<UnitGroupDTO>>;
public sealed class GetUnitGroupByIdQueryHandler(IUnitGroupRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetUnitGroupByIdQuery, BaseResponse<UnitGroupDTO>> 
{
    public async Task<BaseResponse<UnitGroupDTO>> Handle(GetUnitGroupByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new UnitGroupDTO { Id = x.Id, Name = x.Name });
        return result == null ? Failure<UnitGroupDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}