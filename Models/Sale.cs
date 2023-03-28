using System;
using System.Collections.Generic;
using API.Models;

namespace API
{
    public partial class Sale
    {
        public int Id { get; set; }
        public DateTime DateCalculation { get; set; }
        public int Idmanager { get; set; }
        public int IdcurrencyCalculation { get; set; }

        public virtual Employee IdmanagerNavigation { get; set; } = null!;
    }
}
