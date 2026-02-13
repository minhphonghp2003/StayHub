using System.Net;
using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Property;

public record GetPropertyByIdQuery(int Id) : IRequest<BaseResponse<PropertyDTO>>;

internal sealed class GetPropertyByIdQueryHandler(IPropertyRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetPropertyByIdQuery, BaseResponse<PropertyDTO>>
{
    public async Task<BaseResponse<PropertyDTO>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindOneAsync(e => e.Id == request.Id, e => new PropertyDTO { Id = e.Id });
        return result == null ? Failure<PropertyDTO>("Not found",HttpStatusCode.BadRequest) : Success(result);
    }
}
