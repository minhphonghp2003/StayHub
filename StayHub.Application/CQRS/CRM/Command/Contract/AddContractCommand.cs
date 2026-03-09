using MediatR;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.CRM;
using StayHub.Application.Interfaces.Repository.CRM;
using StayHub.Application.Interfaces.Repository.PMM;
using StayHub.Domain.Entity.CRM;
namespace StayHub.Application.CQRS.CRM.Command.Contract;

public record AddContractCommand(List<int> customerIds, List<int>? services, List<ContractAssetDTO>? assets, int representativeId, int UnitId, long Price, long Deposit, long? DepositRemain, DateTime? DepositRemainEndDate, DateTime StartDate, DateTime EndDate, int PaymentPeriodId, string? Note, string? Attachment, bool IsSigned, int? TemplateId, int VehicleNumber, int? SaleId) : IRequest<BaseResponse<bool>>;
public sealed class AddContractCommandHandler(IContractRepository repository,IUnitRepository unitRepository, ICustomerRepository customerRepository, IServiceRepository serviceRepository, IAssetRepository assetRepository) : BaseResponseHandler, IRequestHandler<AddContractCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(AddContractCommand request, CancellationToken ct)
    {
        var unit = await unitRepository.GetEntityByIdAsync(request.UnitId);
        if(unit == null || unit.IsActive == false)
        {

            return Failure<bool>("Không tìm thấy phòng", System.Net.HttpStatusCode.BadRequest);
        }
        if (unit.MaximumCustomer < request.customerIds.Count())
        {

            return Failure<bool>("Vượt quá số khách thuê giới hạn", System.Net.HttpStatusCode.BadRequest);
        }
        if ((await repository.FindOneAsync(e => e.UnitId == request.UnitId , selector: e => e)) != null)
        {
            return Failure<bool>("Phòng không có sẵn", System.Net.HttpStatusCode.BadRequest);
        }
        if (request.customerIds.Count == 0 || !request.customerIds.Contains(request.representativeId))
        {
            return Failure<bool>("Cần phải có khách hàng đại diện", System.Net.HttpStatusCode.BadRequest);
        }
        
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
            filter: e => request.customerIds.Contains(e.Id) && e.ContractId == null
        )).ToList();

        foreach (var customer in customers)
        {
            customer.IsRepresentative = (customer.Id == request.representativeId);
        }
        entity.Customers = customers;

        // 2. Process Services (Optimized Lookup)
        if (request.services?.Any() == true)
        {
            var validService = (await serviceRepository.GetManyEntityAsync(
                filter: e => e.Property.UnitGroups.Any(ug => ug.Units.Any(u => u.Id == request.UnitId)) && request.services.Contains(e.Id)
            )).ToHashSet();
            entity.Services = validService.ToList();
        }

        // 3. Process Assets (Optimized Lookup)
        if (request.assets?.Any() == true)
        {
            var requestedAssetIds = request.assets.Select(a => a.AssetId).ToHashSet();
            var validAssetIds = (await assetRepository.GetManyAsync(
                filter: e => requestedAssetIds.Contains(e.Id),
                selector: (e, i) => e.Id
            )).ToHashSet();

            entity.ContractAssets = request.assets
                .Where(a => validAssetIds.Contains(a.AssetId))
                .Select(a => new ContractAsset
                {
                    AssetId = a.AssetId,
                    Quantity = a.Quantity,
                }).ToList();
        }

        await repository.AddAsync(entity);
        return Success(true);
    }
}