namespace LogDump.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public AddressType AddressType { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
