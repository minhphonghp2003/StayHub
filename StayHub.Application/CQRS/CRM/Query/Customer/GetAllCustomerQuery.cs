using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.Customer;
public record GetAllCustomerQuery(int propertyId, int? pageNumber, int? pageSize, string? searchKey) : IRequest<Response<CustomerDTO>>;
public sealed class GetAllCustomerQueryHandler(ICustomerRepository repository, IConfiguration config) : BaseResponseHandler, IRequestHandler<GetAllCustomerQuery, Response<CustomerDTO>> 
{
    public async Task<Response<CustomerDTO>> Handle(GetAllCustomerQuery request, CancellationToken ct) 
    {
        var size = request.pageSize ?? config.GetValue<int>("PageSize");
        var (result, count) = await repository.GetManyPagedAsync(
            pageNumber: request.pageNumber ?? 1,
            pageSize: size,
            filter: x => x.PropertyId==request.propertyId&& request.searchKey == null || x.Name.Contains(request.searchKey),
            selector: (x, i) => new CustomerDTO 
            { 
                Id = x.Id, 
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                CCCD = x.CCCD,
                GenderId = x.GenderId,
                Gender = new DTO.Catalog.CategoryItemDTO
                {
                    Id =x.Gender.Id,
                    Name = x.Gender.Name
                       
                },
                ProvinceId = x.ProvinceId,
                Province = new DTO.Catalog.ProvinceDTO
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                },

                WardId = x.WardId,
                Ward = new DTO.Catalog.WardDTO
                {
                    Id = x.Ward.Id,
                    Name = x.Ward.Name,
                },
                UnitId = x.UnitId,
                Unit = new DTO.PMM.UnitDTO
                {
                    Id = x.Unit.Id,
                    Name = x.Unit.Name,
                },
                DateOfBirth = x.DateOfBirth,
                Address = x.Address,
                Image = x.Image,
                Job = x.Job
            }
        );
        return SuccessPaginated(result.ToList(), count,size, request.pageNumber ?? 1);
    }
}