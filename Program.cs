using LoneSpoof.Misc;
using LoneSpoof.Spoofing;
using Spectre.Console;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace LoneSpoof
{
    internal class Program
    {
        static void Main()
        {
            string version = Assembly
                .GetExecutingAssembly()!
                .GetCustomAttribute<AssemblyFileVersionAttribute>()!
                .Version!;
            // Setup Console & Logging
            Console.OutputEncoding = Encoding.UTF8;
            using var logger = new ConsoleLogWriter(path: $"Log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
            try
            {
                // Render Header
                var title = new FigletText("LoneSpoof")
                    .Centered()
                    .Color(Color.Aqua);
                AnsiConsole.Write(title);

                AnsiConsole.MarkupLine($"[gray]Version {version}[/]");
                AnsiConsole.MarkupLine($"[gray]https://lone-dma.org/[/]");
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                AnsiConsole.WriteLine();
                // Startup Amidewin and get current values
                AnsiConsole.MarkupLine("[bold green]Initializing Amidewin...[/]");
                Amidewin.Init();
                AnsiConsole.MarkupLine("[bold green]Amidewin initialized successfully.[/]");
                AnsiConsole.MarkupLine("[bold green]Retrieving BIOS values...[/]");
                var keys = new List<AmidewinKeyValuePair>();
                foreach (var descriptor in Amidewin.Descriptors)
                {
                    keys.Add(Amidewin.GetValue(descriptor.Key));
                }

                // Confirm new values with user
                AnsiConsole.WriteLine();
                foreach (var key in keys)
                {
                    // Print descriptor info
                    key.Descriptor.Print();

                    // Show current value
                    AnsiConsole.MarkupLine($"[bold]Current Value:[/] [yellow]{Markup.Escape(key.OriginalValue)}[/]");

                    // Prompt for new value
                    string input = AnsiConsole.Prompt(
                        new TextPrompt<string>($"Enter new value for [yellow]{Markup.Escape(key.Descriptor.Name)} ({Markup.Escape(key.Key)})[/]")
                            .DefaultValue(key.Descriptor.DefaultSpoofedValue ?? key.OriginalValue)
                            .AllowEmpty());

                    ArgumentException.ThrowIfNullOrWhiteSpace(input, nameof(input)); // extra safety
                    key.SpoofedValue = input.Trim();

                    AnsiConsole.WriteLine();
                }

                // Show all changed values and confirm
                AnsiConsole.MarkupLine("[bold underline]Summary of Changes:[/]\n");
                foreach (var key in keys)
                {
                    key.PrintChange();
                }
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[blue]Check the values above and make sure everything looks good.[/]");
                AnsiConsole.MarkupLine("[grey]If there are problems open an Issue on GitHub, and I'll try to take a look at it.[/]");
                bool confirm = AnsiConsole.Confirm("Do you want to proceed with these values?");
                if (!confirm)
                {
                    AnsiConsole.MarkupLine("[red]Operation cancelled by user.[/]");
                    return;
                }
                bool confirm2 = AnsiConsole.Confirm("Are you sure?");
                if (!confirm2)
                {
                    AnsiConsole.MarkupLine("[red]Operation cancelled by user.[/]");
                    return;
                }

                // Begin spoofing
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[bold green]Proceeding with spoofed values...[/]");
                foreach (var key in keys)
                {
                    key.Spoof();
                }
                AnsiConsole.MarkupLine("[bold yellow]Spoofing completed. Flash your BIOS and reinstall Windows.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An unhandled exception occurred: {Markup.Escape(ex.Message)}[/]");
            }
            finally
            {
                AnsiConsole.MarkupLine("[grey]Press any key to exit...[/]");
                logger.Dispose();
                Console.ReadKey(intercept: true);
            }
        }
    }
}
