using System;

namespace LogDump.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public Tuple<string, int, decimal> Level { get; set; }

        public Account Account { get; set; }

        public Address Address { get; set; }
    }
}
