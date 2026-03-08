using MediatR;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Domain.Entity.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Contract;

public record AddContractCommand(List<int> customerIds, List<ContractServiceDTO>? services, List<ContractAssetDTO>? assets, int representativeId, int UnitId, long Price, long Deposit, long? DepositRemain, DateTime? DepositRemainEndDate, DateTime StartDate, DateTime EndDate, int PaymentPeriodId, string? Note, string? Attachment, bool IsSigned, int? TemplateId, int VehicleNumber, int? SaleId) : IRequest<BaseResponse<bool>>;
public sealed class AddContractCommandHandler(IContractRepository repository, ICustomerRepository customerRepository, IServiceRepository serviceRepository, IAssetRepository assetRepository) : BaseResponseHandler, IRequestHandler<AddContractCommand, BaseResponse<bool>>
{
    //TODO  add service, add asset
    public async Task<BaseResponse<bool>> Handle(AddContractCommand request, CancellationToken ct)
    {
        var entity = new StayHub.Domain.Entity.CRM.Contract
        {
            UnitId = request.UnitId,
            Code = $"HD{Random.Shared.Next(10000, 100000)}",
            Status = ContractStatus.Pending,
            IsSigned = request.IsSigned,
            VehicleNumber = request.VehicleNumber,
            Price = request.Price,
            Deposit = request.Deposit,
            DepositRemain = request.DepositRemain,
            DepositRemainEndDate = request.DepositRemainEndDate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PaymentPeriodId = request.PaymentPeriodId,
            Note = request.Note,
            Attachment = request.Attachment,
            TemplateId = request.TemplateId,
            SaleId = request.SaleId,
        };

        // 1. Process Customers (Filter unassigned customers only)
        var customers = (await customerRepository.GetManyEntityAsync(
            filter: e => request.customerIds.Contains(e.Id) && e.UnitId == null
        )).ToList();

        foreach (var customer in customers)
        {
            customer.IsRepresentative = (customer.Id == request.representativeId);
            customer.UnitId = request.UnitId;   
        }
        entity.Customers = customers;

        // 2. Process Services (Optimized Lookup)
        if (request.services?.Any() == true)
        {
            var requestedServiceIds = request.services.Select(s => s.Id).ToHashSet();
            var validServiceIds = (await serviceRepository.GetManyAsync(
                filter: e => requestedServiceIds.Contains(e.Id),
                selector: (e, i) => e.Id
            )).ToHashSet();

            entity.ContractServices = request.services
                .Where(s => validServiceIds.Contains(s.Id))
                .Select(s => new ContractService
                {
                    ServiceId = s.Id,
                    Quantity = s.Quantity,
                }).ToList();
        }

        // 3. Process Assets (Optimized Lookup)
        if (request.assets?.Any() == true)
        {
            var requestedAssetIds = request.assets.Select(a => a.Id).ToHashSet();
            var validAssetIds = (await assetRepository.GetManyAsync(
                filter: e => requestedAssetIds.Contains(e.Id),
                selector: (e, i) => e.Id
            )).ToHashSet();

            entity.ContractAssets = request.assets
                .Where(a => validAssetIds.Contains(a.Id))
                .Select(a => new ContractAsset
                {
                    AssetId = a.Id,
                    Quantity = a.Quantity,
                }).ToList();
        }

        await repository.AddAsync(entity);
        return Success(true);
    }
}