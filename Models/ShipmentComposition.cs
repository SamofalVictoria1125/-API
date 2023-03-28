using System;
using System.Collections.Generic;

namespace API
{
    public partial class ShipmentComposition
    {
        public int Id { get; set; }
        public int Idshipment { get; set; }
        public int Idproduct { get; set; }
        public int Quantity { get; set; }
        public int Volume { get; set; }
        public int Weight { get; set; }
        public int Sum { get; set; }

        public virtual Product IdproductNavigation { get; set; } = null!;
        public virtual Shipment IdshipmentNavigation { get; set; } = null!;
    }
}
