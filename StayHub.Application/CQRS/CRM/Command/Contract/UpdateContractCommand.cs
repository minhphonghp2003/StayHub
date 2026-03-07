using MediatR;
using Shared.Response;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Domain.Entity.CRM;
using System.Net;
using System.Text.Json.Serialization;
namespace StayHub.Application.CQRS.CRM.Command.Contract;

public class UpdateContractCommand : IRequest<BaseResponse<ContractDTO>>
{
    [JsonIgnore] public int Id { get; set; }
    public int UnitId { get; set; }
    public long Price { get; set; }
    public long Deposit { get; set; }
    public long? DepositRemain { get; set; }
    public DateTime? DepositRemainEndDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PaymentPeriodId { get; set; }
    public string? Note { get; set; }
    public string? Attachment { get; set; }
    public string Code { get; set; }
    public bool IsSigned { get; set; }
    public int? TemplateId { get; set; }
    public List<ContractServiceDTO>? services { get; set; }
    public List<ContractAssetDTO>? assets { get; set; }
    public List<int> customerIds { get; set; }
    public int representativeId { get; set; }
}
public sealed class UpdateContractCommandHandler(IContractRepository repository, ICustomerRepository customerRepository) : BaseResponseHandler, IRequestHandler<UpdateContractCommand, BaseResponse<ContractDTO>>
{
    public async Task<BaseResponse<ContractDTO>> Handle(UpdateContractCommand request, CancellationToken ct)
    {
        // 1. Fetch the entity with its collections (Ensure your Repo/EF includes these)
        var entity = await repository.FindOneEntityAsync(e => e.Id == request.Id);
        if (entity == null) return Failure<ContractDTO>("Contract not found", HttpStatusCode.NotFound);

        // 2. Update Basic Properties
        entity.Price = request.Price;
        entity.Deposit = request.Deposit;
        entity.DepositRemain = request.DepositRemain;
        entity.DepositRemainEndDate = request.DepositRemainEndDate;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.PaymentPeriodId = request.PaymentPeriodId;
        entity.Note = request.Note;
        entity.Attachment = request.Attachment;
        entity.Code = request.Code;
        entity.IsSigned = request.IsSigned;
        entity.TemplateId = request.TemplateId;

        if (request.customerIds != null)
        {
            // Identify customers to remove (those currently in entity but NOT in request)
            var requestedCustomerIds = request.customerIds.ToHashSet();

            // Before removing, reset the IsRepresentative flag for those being removed
            var customersToRemove = entity.Customers.Where(c => !requestedCustomerIds.Contains(c.Id)).ToList();
            foreach (var c in customersToRemove)
            {
                c.IsRepresentative = false;
            }

            entity.Customers.RemoveAll(c => !requestedCustomerIds.Contains(c.Id));

            // Add new customers that aren't already linked
            var existingCustomerIds = entity.Customers.Select(c => c.Id).ToHashSet();
            var newCustomerIds = request.customerIds.Where(id => !existingCustomerIds.Contains(id)).ToList();

            if (newCustomerIds.Any())
            {
                var newCustomers = await customerRepository.GetManyEntityAsync(c => newCustomerIds.Contains(c.Id));
                entity.Customers.AddRange(newCustomers);
            }

            foreach (var customer in entity.Customers)
            {
                customer.IsRepresentative = (customer.Id == request.representativeId);
            }
        }
        // 3. Sync Services (Add, Update, Remove)
        if (request.services != null)
        {
            // Remove existing services that are not in the new request
            var requestedServiceIds = request.services.Select(s => s.Id).ToHashSet();
            entity.ContractServices.RemoveAll(s => !requestedServiceIds.Contains(s.ServiceId));

            foreach (var serviceDto in request.services)
            {
                var existingService = entity.ContractServices.FirstOrDefault(s => s.ServiceId == serviceDto.Id);
                if (existingService != null)
                {
                    existingService.Quantity = serviceDto.Quantity;
                }
                else
                {
                    entity.ContractServices.Add(new ContractService
                    {
                        ServiceId = serviceDto.Id,
                        Quantity = serviceDto.Quantity
                    });
                }
            }
        }

        // 4. Sync Assets (Add, Update, Remove)
        if (request.assets != null)
        {
            // Remove existing assets that are not in the new request
            var requestedAssetIds = request.assets.Select(a => a.Id).ToHashSet();
            entity.ContractAssets.RemoveAll(a => !requestedAssetIds.Contains(a.AssetId));

            foreach (var assetDto in request.assets)
            {
                var existingAsset = entity.ContractAssets.FirstOrDefault(a => a.AssetId == assetDto.Id);
                if (existingAsset != null)
                {
                    existingAsset.Quantity = assetDto.Quantity;
                }
                else
                {
                    entity.ContractAssets.Add(new ContractAsset
                    {
                        AssetId = assetDto.Id,
                        Quantity = assetDto.Quantity
                    });
                }
            }
        }

        repository.Update(entity);
        return Success(new ContractDTO
        {
            Id = entity.Id,
            UnitId = entity.UnitId,
            Price = entity.Price,
            Deposit = entity.Deposit,
            DepositRemain = entity.DepositRemain,
            DepositRemainEndDate = entity.DepositRemainEndDate,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            PaymentPeriodId = entity.PaymentPeriodId,
            Note = entity.Note,
            Attachment = entity.Attachment,
            Code = entity.Code,
            IsSigned = entity.IsSigned,
            TemplateId = entity.TemplateId
        });
    }
}