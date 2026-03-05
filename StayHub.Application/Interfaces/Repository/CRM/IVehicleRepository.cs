using Vehicle = StayHub.Domain.Entity.CRM.Vehicle;
namespace StayHub.Application.Interfaces.Repository.CRM;
public interface IVehicleRepository : IPagingAndSortingRepository<Vehicle> { }