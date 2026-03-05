using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
namespace StayHub.Application.CQRS.PMM.Query.Service;
public record GetServiceByIdQuery(int Id) : IRequest<BaseResponse<ServiceDTO>>;
public sealed class GetServiceByIdQueryHandler(IServiceRepository repository) : BaseResponseHandler, IRequestHandler<GetServiceByIdQuery, BaseResponse<ServiceDTO>> 
{
    public async Task<BaseResponse<ServiceDTO>> Handle(GetServiceByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new ServiceDTO 
        { 
            Id = x.Id, 
            Name = x.Name,
            PropertyId = x.PropertyId,
            IsActive = x.IsActive,
            Description = x.Description
        });
        return result == null ? Failure<ServiceDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}