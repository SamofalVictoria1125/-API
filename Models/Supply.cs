using System;
using System.Collections.Generic;

namespace API
{
    public partial class Supply
    {
        public Supply()
        {
            DeliveryCompositions = new HashSet<DeliveryComposition>();
        }

        public int Id { get; set; }
        public DateTime SupplyDate { get; set; }
        public int Idmanager { get; set; }

        public virtual Employee IdmanagerNavigation { get; set; } = null!;
        public virtual ICollection<DeliveryComposition> DeliveryCompositions { get; set; }
    }
}
