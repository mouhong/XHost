# XHost #

## What is XHost? ##
XHost is a command line tool to manage Windows hosts file.

## System Requirements ##
.NET Framework 3.5+

## How to Use? ##
1. Add the containing directory of `xhost.exe` to PATH environment varible;
2. Open command line window (cmd.exe), type `xhost -help`;

## Samples ##

1. `xhost -ls`: List all entries in the hosts file;
2. `xhost -find test.com`: Find the entry whose host is `test.com`;
3. `xhost -add 127.0.0.1 test.com`: Add a new entry to the hosts file;
4. `xhost -update 127.0.0.1 test.com`: Update the `test.com` entry;
5. `xhost -rm test1.com`: Remove host `test1.com` from hosts file;
6. `xhost -rm test1.com, test2.com`: Revmoe hosts `test1.com` and `test2.com` from hosts file;
7. `xhost -h	`: Show help;

## Extensibility ##

Custom commands can be added by writing plugins. A plugin in XHost is a command class implementing `XHost.Commands.ICommand`. To write a plugin, you should:

1. Write a class implementing `XHost.Commands.ICommand` interface;
2. Compile the plugin;
3. Drop the dll file to the `Plugins` folder (please create it if not exists);

### Sample Plugin: NuGet Plugin ###

NuGet is sometimes blocked in China. In this sample plugin, we will add a entry for nuget.org to the hosts file.

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
        get { return "Add 157.56.8.150 nuget.org to hosts file"; }
    }

    public string Usage
    {
        get { return "xhost -nuget"; }
    }

    public void Execute(CommandLine commandLine, CommandExecutionContext context)
    {
        context.HostFile.Set("157.56.8.150", "nuget.org");
        context.HostFile.Save();

        context.Output.WriteLine("OK");
    }
}
```

**Note:** The `Description` and `Usage` properties are only used in the help.

Then you can use the following command to add host entry for nuget.org:

`xhost -nuget`

