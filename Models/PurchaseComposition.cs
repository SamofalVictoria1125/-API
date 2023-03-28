﻿using System;
using System.Collections.Generic;
using API.Models;

namespace API
{
    public partial class PurchaseComposition
    {
        public int Id { get; set; }
        public int Idpurchase { get; set; }
        public int Idproduct { get; set; }
        public int Quantity { get; set; }
        public int Volume { get; set; }
        public int Weight { get; set; }
        public int Sum { get; set; }

        public virtual Product IdproductNavigation { get; set; } = null!;
        public virtual Purchase IdpurchaseNavigation { get; set; } = null!;
    }
}
