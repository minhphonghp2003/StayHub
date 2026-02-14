namespace StayHub.Application.DTO.TMS;

public class UnitGroupDTO : BaseDTO
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int PropertyId { get; set; }
     public List<UnitDTO> Units { get; set; }
     public PropertyDTO Property { get; set; }
}
