using System;
using System.Collections.Generic;

namespace API
{
    public partial class Employee
    {
        public Employee()
        {
            Purchases = new HashSet<Purchase>();
            Sales = new HashSet<Sale>();
            Shipments = new HashSet<Shipment>();
            Supplies = new HashSet<Supply>();
        }

        public int Id { get; set; }
        public int IdcontactPerson { get; set; }

        public virtual ContactPerson IdcontactPersonNavigation { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
