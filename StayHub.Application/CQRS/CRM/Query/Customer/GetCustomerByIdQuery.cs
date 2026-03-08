using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using Microsoft.AspNetCore.Http;
using StayHub.Application.Extension;
namespace StayHub.Application.CQRS.CRM.Query.Customer;

public record GetCustomerByIdQuery(int Id) : IRequest<BaseResponse<CustomerDTO>>;
public sealed class GetCustomerByIdQueryHandler(ICustomerRepository repository, IHttpContextAccessor httpContextAccessor) : BaseResponseHandler, IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerDTO>>
{
    public async Task<BaseResponse<CustomerDTO>> Handle(GetCustomerByIdQuery request, CancellationToken ct)
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id && x.Property.Users.Any(u => u.Id == httpContextAccessor.HttpContext.GetUserId()), (x) => new CustomerDTO
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
        });
        return result == null ? Failure<CustomerDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}