using System;
using System.Collections.Generic;

namespace API
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseCompositions = new HashSet<PurchaseComposition>();
        }

        public int Id { get; set; }
        public DateTime DateCalculation { get; set; }
        public int Idmanager { get; set; }

        public virtual Employee IdmanagerNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseComposition> PurchaseCompositions { get; set; }
    }
}
