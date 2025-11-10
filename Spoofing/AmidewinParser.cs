using LoneSpoof.Spoofing;
using System.Text.RegularExpressions;

namespace LoneSpoof.Native
{
    internal static partial class AmidewinParser
    {
        public static AmidewinKeyValuePair Parse(string rawAmidewinOutput)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(rawAmidewinOutput, nameof(rawAmidewinOutput));

            foreach (Match match in AmidewinRegex().Matches(rawAmidewinOutput))
            {
                var key = match.Groups["Flag"].Value.Trim();
                var value = match.Groups["Value"].Value.Trim();

                if (!string.IsNullOrEmpty(key))
                {
                    return new AmidewinKeyValuePair(key, value);
                }
            }

            throw new FormatException("No valid Amidewin key-value pairs found in the provided output.");
        }

        [GeneratedRegex(@"^\((?<Flag>/[A-Z0-9]+)\)(?:.*?)""(?<Value>.*?)""", RegexOptions.Multiline | RegexOptions.Compiled)]
        private static partial Regex AmidewinRegex();
    }
}
