using System;
using System.Collections.Generic;
using LogDump.Entities;
using ObjectDump;

namespace LogDump
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new Account
            {
                Id = 1,
                Type = AccountType.Current,
                Code = "AccOne",
                CreatedOn = DateTime.UtcNow,
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Id = 1,
                        Code = "ContOne",
                        Level = new Tuple<string, int, decimal>("One", 1, 1.1M),
                        Address = new Address
                        {
                            Id = 1,
                            AddressType = AddressType.Home,
                            City = "Bangalore",
                            Country = "India",
                            Street = "1-134, Bala",
                        }
                    },
                    new Contact
                    {
                        Id = 2,
                        Code = "ContTwo",
                        Level = new Tuple<string, int, decimal>("Two", 2, 2.2M),
                        Address = new Address
                        {
                            Id = 2,
                            AddressType = AddressType.Work,
                            City = "New York",
                            Country = "USA",
                            Street = "1-134, Grant",
                        }
                    },
                    new Contact
                    {
                        Id = 3,
                        Code = "ContThree",
                        Level = new Tuple<string, int, decimal>("Three", 3, 3.3M),
                        Address = new Address
                        {
                            Id = 3,
                            AddressType = AddressType.Home,
                            City = "Sydney",
                            Country = "Australia",
                            Street = "1-134, Kentachy",
                        }
                    },
                }
            };

            foreach (var contact in account.Contacts)
            {
                contact.Account = account;
            }

            var obj = account.Dump(x =>
            {
                x.UseTypeFullName = true;
                x.Depth = 2;
                x.IndentSize = 2;
                x.IndentChar = '-';
            });

            Console.WriteLine(obj.ToString());
        }
    }
}
