using System;
using System.Collections.Generic;

namespace API
{
    public partial class ContactPerson
    {
        public ContactPerson()
        {
            Counterparties = new HashSet<Counterparty>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Sex { get; set; } = null!;

        public virtual ICollection<Counterparty> Counterparties { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
