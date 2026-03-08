using MediatR;
using Shared.Response;
using Microsoft.Extensions.Configuration;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using Microsoft.EntityFrameworkCore;
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
            filter: x => x.PropertyId == request.propertyId && request.searchKey == null || x.Name.Contains(request.searchKey),
            include: x => x.Include(j => j.Gender).Include(j => j.Province).Include(j => j.Ward).Include(j => j.Contract).ThenInclude(j=>j.Unit),
            selector: (x, i) => new CustomerDTO
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                CCCD = x.CCCD,
                GenderId = x.GenderId,
                Gender = x.Gender == null ? null : new DTO.Catalog.CategoryItemDTO
                {
                    Id = x.Gender.Id,
                    Name = x.Gender.Name

                },
                ProvinceId = x.ProvinceId,
                Province = x.Province == null ? null : new DTO.Catalog.ProvinceDTO
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                },

                WardId = x.WardId,
                Ward = x.Ward == null ? null : new DTO.Catalog.WardDTO
                {
                    Id = x.Ward.Id,
                    Name = x.Ward.Name,
                },
                Unit = x.Contract != null ? new DTO.PMM.UnitDTO
                {
                    Id = x.Contract.Unit.Id,
                    Name = x.Contract.Unit.Name,
                } : null,
                DateOfBirth = x.DateOfBirth,
                Address = x.Address,
                Image = x.Image,
                Job = x.Job
            }
        );
        return SuccessPaginated(result.ToList(), count, size, request.pageNumber ?? 1);
    }
}