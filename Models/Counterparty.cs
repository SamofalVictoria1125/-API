using System;
using System.Collections.Generic;

namespace API
{
    public partial class Counterparty
    {
        public Counterparty()
        {
            Customers = new HashSet<Customer>();
            Sellers = new HashSet<Seller>();
        }

        public int Id { get; set; }
        public string NameOrganization { get; set; } = null!;
        public int IdcontactPerson { get; set; }
        public string Address { get; set; } = null!;

        public virtual ContactPerson IdcontactPersonNavigation { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
    }
}
