using MediatR;
using Shared.Response;
using StayHub.Application.DTO.TMS;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier;

public record RenewSubscriptionCommand(int propertyId, int tierId, DateTime startTime, DateTime endTime ) : IRequest<BaseResponse<bool>>;

public sealed class RenewSubscriptionCommandHandler(IPropertyRepository propertyRepository) 
    : BaseResponseHandler, IRequestHandler<RenewSubscriptionCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(RenewSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var property = await propertyRepository.FindOneEntityAsync(e=>e.Id== request.propertyId);
        if (property == null)
        {
            return Failure<bool>("Property not found", System.Net.HttpStatusCode.BadRequest);
        }

        if (property.EndSubscriptionDate > DateTime.UtcNow)
        {
            return Failure<bool>("Current subscription is still active. Please wait until it expires before renewing.",
                System.Net.HttpStatusCode.BadRequest);
        }

        if (request.startTime >= request.endTime)
        {
            return Failure<bool>("Start time must be before end time.", System.Net.HttpStatusCode.BadRequest);
        }

        if (request.startTime < DateTime.UtcNow)
        {
            return Failure<bool>("Start time cannot be in the past.", System.Net.HttpStatusCode.BadRequest);
        }

        property.StartSubscriptionDate = request.startTime;
        property.EndSubscriptionDate = request.endTime;
        property.TierId = request.tierId;
        propertyRepository.Update(property);
        return Success(true);
    }
}
