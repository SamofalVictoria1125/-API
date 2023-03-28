using System;
using System.Collections.Generic;

namespace API
{
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentCompositions = new HashSet<ShipmentComposition>();
        }

        public int Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int Idmanager { get; set; }

        public virtual Employee IdmanagerNavigation { get; set; } = null!;
        public virtual ICollection<ShipmentComposition> ShipmentCompositions { get; set; }
    }
}
