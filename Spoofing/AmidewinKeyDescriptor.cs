using Spectre.Console;

namespace LoneSpoof.Spoofing
{
    public sealed class AmidewinKeyDescriptor
    {
        public string Key { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string DefaultSpoofedValue { get; init; }

        public void Print()
        {
            AnsiConsole.MarkupLine($"[bold green]{Markup.Escape(Name)} ({Markup.Escape(Key)})[/]\n\n[grey]{Markup.Escape(Description)}[/]");
            AnsiConsole.WriteLine();
        }
    }
}
