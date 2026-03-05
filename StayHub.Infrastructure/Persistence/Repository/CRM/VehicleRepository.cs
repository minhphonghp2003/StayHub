using StayHub.Application.Interfaces.Repository.CRM;
using Vehicle = StayHub.Domain.Entity.CRM.Vehicle;
namespace StayHub.Infrastructure.Persistence.Repository.CRM;
public class VehicleRepository(AppDbContext context) : PagingAndSortingRepository<Vehicle>(context), IVehicleRepository { }