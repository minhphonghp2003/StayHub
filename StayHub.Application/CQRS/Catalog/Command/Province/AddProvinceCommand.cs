using MediatR;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Province;

public record AddProvinceCommand(string Name, string? Code) : IRequest<BaseResponse<ProvinceDTO>>;

public sealed class AddProvinceCommandHandler(IProvinceRepository repository) 
    : BaseResponseHandler, IRequestHandler<AddProvinceCommand, BaseResponse<ProvinceDTO>>
{
    public async Task<BaseResponse<ProvinceDTO>> Handle(AddProvinceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entity.Catalog.Province
        {
            Name = request.Name,
            Code = request.Code
        };
        await repository.AddAsync(entity);
        return Success(new ProvinceDTO { Id = entity.Id, Name = entity.Name, Code = entity.Code });
    }
}
