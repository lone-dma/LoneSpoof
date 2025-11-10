using Spectre.Console;

namespace LoneSpoof.Spoofing
{
    public sealed class AmidewinKeyValuePair
    {
        public AmidewinKeyDescriptor Descriptor { get; }
        public string Key { get; }
        public string OriginalValue { get; }
        public string SpoofedValue { get; set; }
        public bool HasChanged => !string.Equals(OriginalValue, SpoofedValue);

        private AmidewinKeyValuePair() { }

        public AmidewinKeyValuePair(string key, string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
            Key = key.Trim();
            OriginalValue = value.Trim();
            Descriptor = Amidewin.Descriptors[Key];
        }

        public void PrintChange()
        {
            AnsiConsole.MarkupLine($"[bold]{Markup.Escape(Descriptor.Name)} ({Markup.Escape(Key)})[/]: [yellow]'{Markup.Escape(OriginalValue)}'[/] → [green]'{Markup.Escape(SpoofedValue)}'[/]");
        }

        public void Spoof()
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(SpoofedValue, nameof(SpoofedValue));
            Amidewin.SetValue(this);
        }
    }
}
