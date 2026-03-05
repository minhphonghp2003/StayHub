using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.Catalog;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Extension;
using StayHub.Application.Interfaces.Repository.Catalog;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Application.Interfaces.Repository.RBAC;
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
    ) : IRequest<BaseResponse<bool>>;

public sealed class AddPropertyCommandHandler(IPropertyRepository repository,ICategoryRepository categoryRepository,IUserRepository userRepository, IHttpContextAccessor contextAccessor) 
    : BaseResponseHandler, IRequestHandler<AddPropertyCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
    {
        if (!categoryRepository.ExistItemAsync(request.TypeId, CategoryCode.PROPERTY_TYPE))
        {
            return Failure<bool>(message: "Invalid Type", code: System.Net.HttpStatusCode.BadRequest);
        }
        if (request.SubscriptionStatusId.HasValue && !categoryRepository.ExistItemAsync(request.SubscriptionStatusId.Value, CategoryCode.SUBSCRIPTION_STATUS))
        {
            return Failure<bool>(message: "Invalid Subscription Status", code: System.Net.HttpStatusCode.BadRequest);
            
        } 
        if(request.StartSubscriptionDate.HasValue && request.EndSubscriptionDate.HasValue && request.StartSubscriptionDate > request.EndSubscriptionDate)
        {
            return Failure<bool>(message: "Start subscription date cannot be after end subscription date.", code: System.Net.HttpStatusCode.BadRequest);
        }
        var entity = new Domain.Entity.PMM.Property
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
            Users = new List<User>()
        };
        var user = new User
        {
            Id = contextAccessor.HttpContext?.GetUserId()??0
        };
        userRepository.Attach(user);
        entity.Users.Add(user); 
        await repository.AddAsync(entity);
        return Success(true);
    }
}
