# XHost #

## What is XHost? ##
XHost is a command line tool to manage Windows hosts file.

## System Requirements ##
.NET Framework 3.5+

## How to Use? ##
1. Add the containing directory of `xhost.exe` to PATH environment variable;
2. Open command line window (cmd.exe), type `xhost -help`;

## Usage Samples ##

1. `xhost -ls`: List all host entries;
2. `xhost -find test.com`: Find the entry whose host is `test.com`;
3. `xhost -add 127.0.0.1 test.com`: Add a new entry to the hosts file;
4. `xhost -update 127.0.0.1 test.com`: Update the `test.com` entry;
5. `xhost -rm test1.com`: Remove host `test1.com` from the hosts file;
6. `xhost -rm test1.com, test2.com`: Remove host `test1.com` and `test2.com` from the hosts file;
7. `xhost -h	`: Show help;

## Extensibility ##

Custom commands can be added by writing plugins. A plugin in XHost is a class implementing `XHost.Commands.ICommand`. To write a plugin, you have to:

1. Write a class implementing `XHost.Commands.ICommand` interface;
2. Compile the plugin;
3. Drop the dll file to the `Plugins` folder (please create it if not exists);

### Sample Plugin: NuGet Plugin ###

NuGet is sometimes blocked in China. In this sample plugin, we will add a host entry for nuget.org.

```csharp
public class NuGetCommand : ICommand
{
    public string Name
    {
        get { return "nuget"; }
    }

    public string ShortName
    {
        get { return null; }
    }

    public string Description
    {
        get { return "Add 157.56.8.150 nuget.org to hosts file."; }
    }

    public string Usage
    {
        get { return "xhost -nuget"; }
    }

    public void Execute(CommandLine commandLine, CommandExecutionContext context)
    {
        context.Hosts.Set("157.56.8.150", "nuget.org");
        context.Hosts.Save();

        Console.WriteLine("OK");
    }
}
```

**Note:** The `Description` and `Usage` properties will be used in the help.

Then you can use `xhost -nuget` to add host entry for nuget.org.
