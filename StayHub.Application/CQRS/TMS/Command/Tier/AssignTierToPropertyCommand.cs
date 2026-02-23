using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.TMS;

namespace StayHub.Application.CQRS.TMS.Command.Tier
{
    // Include properties to be used as input for the command
    public record AssignTierToPropertyCommand(int propertyId, int tierId) : IRequest<Response<bool>>;
    public sealed class AssignTierToPropertyCommandHandler(IPropertyRepository propertyRepository, IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<AssignTierToPropertyCommand, Response<bool>>
    {
        public async Task<Response<bool>> Handle(AssignTierToPropertyCommand request, CancellationToken cancellationToken)
        {
            var property=  await propertyRepository.FindOneEntityAsync(e=>e.Id==request.propertyId);
            if(property == null)
                return Failure<bool>("Property not found.", System.Net.HttpStatusCode.NotFound);
            property.TierId = request.tierId;
            propertyRepository.Update(property);
            return Success(true, "Tier assigned to property successfully.");
        }
    }
}
