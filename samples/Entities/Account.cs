using System;
using System.Collections.Generic;

namespace LogDump.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public AccountType Type { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }
    }
}
