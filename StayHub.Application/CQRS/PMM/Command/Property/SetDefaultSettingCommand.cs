using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Command.Property
{

    public record SetDefaultSettingCommand(int propertyId, DateTime? paymentDate, long? basePrice) : IRequest<BaseResponse<bool>>;

    public sealed class SetDefaultSettingCommandHandler(IPropertyRepository repository)
        : BaseResponseHandler, IRequestHandler<SetDefaultSettingCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(SetDefaultSettingCommand request, CancellationToken cancellationToken)
        {
            var property = await repository.GetEntityByIdAsync(request.propertyId);
            property.DefaultPaymentDate = request.paymentDate ?? property.DefaultPaymentDate;
            property.DefaultBasePrice = request.basePrice ?? property.DefaultBasePrice;
            repository.Update(property);
            return Success(true);
        }
    }
}
