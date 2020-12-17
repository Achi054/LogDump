# LogDump

**LogDump** is a .net utility to serialize C# object to treeview nodes.<br/>
The dump object can also be used for structured logging for better visualization.

## Download and Install

This library is available on NuGet: _https://www.nuget.org/packages/LogDump_.<br/>
Use the following command to install **LogDump** using NuGet package manager console:

```
  PM> Install-Package LogDump
```

## API Usage

The following sample program uses **LogDump** to write C# objects to the console output:

```c#
static void Main(string[] args)
{
    var account = new Account
    {
        Type = AccountType.Current,
        Code = "AccOne",
        CreatedOn = DateTime.UtcNow,
        Contacts = new List<Contact>
        {
            new Contact
            {
                Code = "ContOne",
                Address = "New Jersey, USA"
            },
            new Contact
            {
                Code = "ContTwo"
                Address = "New York, USA"
            },
            new Contact
            {
                Code = "ContThree",
                Address = "Manhattan, USA"
            },
        }
    };

    var obj = account.Dump();

    Console.WriteLine(obj.ToString());
}

//Console Output:
Account
  Code: "AccOne"
  Type: "AccountType.Current"
  CreatedOn: 12/17/2020 10:43:37 AM
  List<Contact>
  Contact
    Code: "ContOne"
    Address: "New Jersey, USA"
  Contact
    Code: "ContTwo"
    Address: "New York, USA"
  Contact
    Id: 3
    Code: "ContThree"
    Address: "Manhattan, USA"
```

**_Dump_** extension method has 2 overloads.

```c#
// Defines the depth of node tree.
account.Dump(2);

// Dump options that specifies the render style.
account.Dump(x =>
{
    x.UseTypeFullName = true;
    x.Depth = 2;
    x.IndentSize = 2;
    x.IndentChar = '-';
});
```

## Detailed build status

| Branch | Build Status                                                                                                       |
| ------ | ------------------------------------------------------------------------------------------------------------------ |
| dev    | ![GitHub Action Build](https://github.com/Achi054/JsonT/workflows/GitHub%20Action%20Build/badge.svg?branch=dev)    |
| master | ![GitHub Action Build](https://github.com/Achi054/JsonT/workflows/GitHub%20Action%20Build/badge.svg?branch=master) |
