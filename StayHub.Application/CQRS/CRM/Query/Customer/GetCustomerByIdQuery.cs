using MediatR;
using Shared.Response;
using System.Net;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Query.Customer;
public record GetCustomerByIdQuery(int Id) : IRequest<BaseResponse<CustomerDTO>>;
public sealed class GetCustomerByIdQueryHandler(ICustomerRepository repository) : BaseResponseHandler, IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerDTO>> 
{
    public async Task<BaseResponse<CustomerDTO>> Handle(GetCustomerByIdQuery request, CancellationToken ct) 
    {
        var result = await repository.FindOneAsync(x => x.Id == request.Id, (x) => new CustomerDTO 
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
        });
        return result == null ? Failure<CustomerDTO>("Not found", HttpStatusCode.BadRequest) : Success(result);
    }
}