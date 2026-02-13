using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Query.Ward;

public record GetWardByIdQuery(int Id) : IRequest<BaseResponse<WardDTO>>;

internal sealed class GetWardByIdQueryHandler(IWardRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetWardByIdQuery, BaseResponse<WardDTO>>
{
    public async Task<BaseResponse<WardDTO>> Handle(GetWardByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new WardDTO { Id = e.Id });
        return result == null ? Failure<WardDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
