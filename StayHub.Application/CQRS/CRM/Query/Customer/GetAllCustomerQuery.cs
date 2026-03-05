using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.Customer;
public record GetAllCustomerQuery(int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<CustomerDTO>>;
public sealed class GetAllCustomerQueryHandler(ICustomerRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllCustomerQuery, Response<CustomerDTO>> 
{
    public async Task<Response<CustomerDTO>> Handle(GetAllCustomerQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => request.searchKey == null || x.Name.Contains(request.searchKey),
            selector: (x, i) => new CustomerDTO 
            { 
                Id = x.Id, 
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                CCCD = x.CCCD,
                GenderId = x.GenderId,
                ProvinceId = x.ProvinceId,
                WardId = x.WardId,
                UnitId = x.UnitId,
                DateOfBirth = x.DateOfBirth,
                Address = x.Address,
                Image = x.Image,
                Job = x.Job
            }
        );
        return SuccessPaginated(result.ToList(), count, request.pageNumber ?? 1, size);
    }
}