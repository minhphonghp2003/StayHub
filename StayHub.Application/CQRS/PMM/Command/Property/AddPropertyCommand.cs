using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Repository.TMS;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.TMS.Command.Property;


public record AddPropertyCommand(
    string Name,
    string Address,
    int TypeId,
    int TierId,
    string? Image,
    DateTime? StartSubscriptionDate,
    DateTime? EndSubscriptionDate,
    int? SubscriptionStatusId,
    DateTime? LastPaymentDate,
    int? WardId,
    int? ProvinceId
    ) : IRequest<BaseResponse<PropertyDTO>>;

public sealed class AddPropertyCommandHandler(IPropertyRepository repository,ICategoryRepository categoryRepository,IUserRepository userRepository, IHttpContextAccessor contextAccessor) 
    : BaseResponseHandler, IRequestHandler<AddPropertyCommand, BaseResponse<PropertyDTO>>
{
    public async Task<BaseResponse<PropertyDTO>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
    {
        if (!categoryRepository.ExistItemAsync(request.TypeId, CategoryCode.PROPERTY_TYPE))
        {
            return Failure<PropertyDTO>(message: "Invalid Type", code: System.Net.HttpStatusCode.BadRequest);
        }
        if (request.SubscriptionStatusId.HasValue && !categoryRepository.ExistItemAsync(request.SubscriptionStatusId.Value, CategoryCode.SUBSCRIPTION_STATUS))
        {
            return Failure<PropertyDTO>(message: "Invalid Subscription Status", code: System.Net.HttpStatusCode.BadRequest);
            
        } 
        var entity = new Domain.Entity.TMS.Property
        {
            
            Name = request.Name,
            Address = request.Address,
            TypeId = request.TypeId,
            Image = request.Image,
            StartSubscriptionDate = request.StartSubscriptionDate,
            EndSubscriptionDate = request.EndSubscriptionDate,
            SubscriptionStatusId = request.SubscriptionStatusId,
            LastPaymentDate = request.LastPaymentDate,
            TierId = request.TierId,
            WardId = request.WardId,
            ProvinceId = request.ProvinceId,
        };
        var user = new User
        {
            Id = contextAccessor.HttpContext?.GetUserId()??0
        };
        userRepository.Attach(user);
        entity.Users.Add(user); 
        await repository.AddAsync(entity);
        return Success(new PropertyDTO
        {
            Id = entity.Id,
            Name = entity.Name, 
            Address = entity.Address,   
            Type = new CategoryItemDTO { Id = entity.Type.Id, Name = entity.Type.Name },
            Image = entity.Image,
        });
    }
}
