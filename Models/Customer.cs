using System;
using System.Collections.Generic;
using API.Models;

namespace API
{
    public partial class Customer
    {
        public int Id { get; set; }
        public int Idpartner { get; set; }

        public virtual Counterparty IdpartnerNavigation { get; set; } = null!;
    }
}
