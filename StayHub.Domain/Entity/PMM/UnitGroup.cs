using StayHub.Domain.Entity.TMS;

namespace StayHub.Domain.Entity.PMM;

public class UnitGroup : BaseEntity
{
        public string Name { get; set; }
        public string Code { get; set; }
        public int PropertyId { get; set; }
        public virtual List<Unit> Units { get; set; }
        public virtual Property Property { get; set; }
}
