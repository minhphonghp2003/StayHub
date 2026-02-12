namespace StayHub.Domain.Entity.Catalog;

public class Ward : BaseEntity
{
    public string Name { get; set; }
    public string? Code { get; set; }
    public int ProvinceId { get; set; }
    public Province Province { get; set; }
}