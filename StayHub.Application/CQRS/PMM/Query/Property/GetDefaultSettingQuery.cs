using MediatR;
using Shared.Response;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.PMM.Query.Property
{

    public record GetDefaultSettingQuery(int Id) : IRequest<BaseResponse<DefaultSettingDTO>>;

    internal sealed class GetPropertyByIdQueryHandler(IPropertyRepository repository)
        : BaseResponseHandler, IRequestHandler<GetDefaultSettingQuery, BaseResponse<DefaultSettingDTO>>
    {
        public async Task<BaseResponse<DefaultSettingDTO>> Handle(GetDefaultSettingQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FindOneAsync(e => e.Id == request.Id, selector: (e) => new DefaultSettingDTO
            {
                DefaultBasePrice = e.DefaultBasePrice,
                DefaultPaymentDate = e.DefaultPaymentDate,
            });
            return result == null ? Failure<DefaultSettingDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
        }
    }
}
