using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Customer;

public record AddCustomerCommand(string Name, string Phone,int PropertyId, string? Email, string? CCCD, int? GenderId, int? ProvinceId, int? WardId,
    int? UnitId, DateTime? DateOfBirth, string? Address, string? Image, string? Job) : IRequest<BaseResponse<bool>>;
public sealed class AddCustomerCommandHandler(ICustomerRepository repository) : BaseResponseHandler, IRequestHandler<AddCustomerCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddCustomerCommand request, CancellationToken ct)
    {
        var entity = new StayHub.Domain.Entity.CRM.Customer
        {
            Name = request.Name,
            Phone = request.Phone,
            PropertyId = request.PropertyId,
            Email = request.Email,
            CCCD = request.CCCD,
            GenderId = request.GenderId,
            ProvinceId = request.ProvinceId,
            WardId = request.WardId,
            UnitId = request.UnitId,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            Image = request.Image,
            Job = request.Job
        };
        await repository.AddAsync(entity);
        return Success(true);
    }
}