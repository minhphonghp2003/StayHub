using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Query.Property;

public record GetAllPropertyQuery() : IRequest<BaseResponse<List<PropertyDTO>>>;

public sealed class GetAllPropertyQueryHandler(IPropertyRepository repository) 
    : BaseResponseHandler, IRequestHandler<GetAllPropertyQuery, BaseResponse<List<PropertyDTO>>>
{
    public async Task<BaseResponse<List<PropertyDTO>>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllAsync(selector: (e, i) => new PropertyDTO { Id = e.Id });
        return Success(result.ToList());
    }
}
