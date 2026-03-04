using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Unit;
public record GetUnitByIdQuery(int Id) : IRequest<BaseResponse<UnitDTO>>;
public sealed class GetUnitByIdQueryHandler(IUnitRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetUnitByIdQuery, BaseResponse<UnitDTO>> 
{
    public async Task<BaseResponse<UnitDTO>> Handle(GetUnitByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new UnitDTO { Id = x.Id, Name = x.Name });
        return result == null ? Failure<UnitDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}