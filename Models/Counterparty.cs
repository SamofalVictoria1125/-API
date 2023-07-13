using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API
{
    public partial class Counterparty
    {


        public int Id { get; set; }
        public string NameOrganization { get; set; } = null!;
        public int IdContactPerson { get; set; }
        public string Address { get; set; } = null!;
    }
}
     
