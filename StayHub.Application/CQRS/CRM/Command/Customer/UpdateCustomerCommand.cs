using MediatR;
using Shared.Response;
using System.Net;
using System.Text.Json.Serialization;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Customer;
public class UpdateCustomerCommand : IRequest<BaseResponse<CustomerDTO>> 
{
    [JsonIgnore] public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int PropertyId { get; set; }
    public string? Email { get; set; }
    public string? CCCD { get; set; }
    public int? GenderId { get; set; }
    public int? ProvinceId { get; set; }
    public int? WardId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    public string? Job { get; set; }
}
public sealed class UpdateCustomerCommandHandler(ICustomerRepository repository) : BaseResponseHandler, IRequestHandler<UpdateCustomerCommand, BaseResponse<CustomerDTO>> 
{
    public async Task<BaseResponse<CustomerDTO>> Handle(UpdateCustomerCommand request, CancellationToken ct) 
    {
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<CustomerDTO>("Not found", HttpStatusCode.BadRequest);
        
        entity.Name = request.Name;
        entity.Phone = request.Phone;
        entity.PropertyId = request.PropertyId;
        entity.Email = request.Email;
        entity.CCCD = request.CCCD;
        entity.GenderId = request.GenderId;
        entity.ProvinceId = request.ProvinceId;
        entity.WardId = request.WardId;
        entity.DateOfBirth = request.DateOfBirth;
        entity.Address = request.Address;
        entity.Image = request.Image;
        entity.Job = request.Job;

        repository.Update(entity);
        return Success(new CustomerDTO 
        { 
            Id = entity.Id, 
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            CCCD = entity.CCCD,
            GenderId = entity.GenderId,
            ProvinceId = entity.ProvinceId,
            WardId = entity.WardId,
            UnitId = entity.UnitId,
            DateOfBirth = entity.DateOfBirth,
            Address = entity.Address,
            Image = entity.Image,
            Job = entity.Job
        });
    }
}