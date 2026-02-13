using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Province;

public class UpdateProvinceCommand : IRequest<BaseResponse<ProvinceDTO>>
{
    public int Id { get; set; }
}

public sealed class UpdateProvinceCommandHandler(IProvinceRepository repository) 
    : BaseResponseHandler, IRequestHandler<UpdateProvinceCommand, BaseResponse<ProvinceDTO>>
{
    public async Task<BaseResponse<ProvinceDTO>> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ProvinceDTO>("Not found", HttpStatusCode.BadRequest);
        
        repository.Update(entity);
        return Success(new ProvinceDTO { Id = entity.Id });
    }
}
