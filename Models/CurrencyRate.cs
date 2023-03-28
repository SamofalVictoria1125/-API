using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class CurrencyRate
    {
        public int Id { get; set; }
        public int Idcurrency { get; set; }
        public string DateRate { get; set; } = null!;
        public string Rate { get; set; } = null!;

        public virtual Currency IdcurrencyNavigation { get; set; } = null!;
    }
}
