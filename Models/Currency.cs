using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyRates = new HashSet<CurrencyRate>();
        }

        public int Id { get; set; }
        public string CurrencyName { get; set; } = null!;

        public virtual ICollection<CurrencyRate> CurrencyRates { get; set; }
    }
}
