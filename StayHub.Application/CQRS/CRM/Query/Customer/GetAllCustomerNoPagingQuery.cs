using MediatR;
using Shared.Response;
using StayHub.Application.DTO.CRM;
using StayHub.Application.DTO.PMM;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Application.Interfaces.Repository.PMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.CRM.Query.Customer
{
    public record GetAllCustomerNoPagingQuery(int propertyId) : IRequest<BaseResponse<List<CustomerDTO>>>;
    public sealed class GetAllCustomerNoPagingQueryHandler(ICustomerRepository repository)
        : BaseResponseHandler, IRequestHandler<GetAllCustomerNoPagingQuery, BaseResponse<List<CustomerDTO>>>
    {
        public async Task<BaseResponse<List<CustomerDTO>>> Handle(GetAllCustomerNoPagingQuery request, CancellationToken ct)
        {
            var result = await repository.GetManyAsync(filter: e => e.PropertyId == request.propertyId, selector: (e, i) => new CustomerDTO
            {
                Id = e.Id,
                Name = e.Name,
                Phone = e.Phone,
            });
            return Success(result.ToList());
        }
    }
}
