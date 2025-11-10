using LoneSpoof.Native;
using Spectre.Console;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace LoneSpoof.Spoofing
{
    internal static class Amidewin
    {
        private const string AMIDEWIN_EXE = "AMIDEWINx64.EXE";
        private const string AMIDEWIN_DRIVER = "amigendrv64.sys";

        public static IReadOnlyDictionary<string, AmidewinKeyDescriptor> Descriptors { get; } = new Dictionary<string, AmidewinKeyDescriptor>(StringComparer.OrdinalIgnoreCase)
        {
            ["/SU"] = new AmidewinKeyDescriptor
            {
                Key = "/SU",
                Name = "System UUID",
                Description = "The universally unique identifier assigned to the system. This is the most important identifier that needs to change. I recommend leaving this as 'AUTO' since it will generate a valid/random replacement, but advanced users can specify a value (16 bytes upper-case HEX).",
                DefaultSpoofedValue = "AUTO"
            },
            ["/SS"] = new AmidewinKeyDescriptor
            {
                Key = "/SS",
                Name = "System Serial Number",
                Description = "The serial number assigned to the system by the manufacturer.",
                DefaultSpoofedValue = "To be filled by O.E.M."
            },
            ["/BS"] = new AmidewinKeyDescriptor
            {
                Key = "/BS",
                Name = "Baseboard Serial Number",
                Description = "The serial number assigned to the baseboard (motherboard) by the manufacturer. You may want to research your Vendor's Serial Number layout to ensure the new value is valid. Otherwise best to leave as default.",
                DefaultSpoofedValue = "Default string"
            },
            ["/SM"] = new AmidewinKeyDescriptor
            {
                Key = "/SM",
                Name = "System Manufacturer",
                Description = "The manufacturer name assigned to the system.",
                DefaultSpoofedValue = "System manufacturer"
            },
            ["/SP"] = new AmidewinKeyDescriptor
            {
                Key = "/SP",
                Name = "System Product Name",
                Description = "The product name assigned to the system.",
                DefaultSpoofedValue = "System Product Name"
            },
            ["/SV"] = new AmidewinKeyDescriptor
            {
                Key = "/SV",
                Name = "System Version",
                Description = "The version string for the system.",
                DefaultSpoofedValue = "System Version"
            },
            ["/SK"] = new AmidewinKeyDescriptor
            {
                Key = "/SK",
                Name = "System SKU Number",
                Description = "The stock keeping unit number for the system.",
                DefaultSpoofedValue = "SKU"
            },
            ["/SF"] = new AmidewinKeyDescriptor
            {
                Key = "/SF",
                Name = "System Family",
                Description = "The family or series designation of the system.",
                DefaultSpoofedValue = "To be filled by O.E.M."
            },
            ["/BT"] = new AmidewinKeyDescriptor
            {
                Key = "/BT",
                Name = "Baseboard Asset Tag",
                Description = "The asset tag assigned to the baseboard.",
                DefaultSpoofedValue = "Default string"
            },
            ["/BLC"] = new AmidewinKeyDescriptor
            {
                Key = "/BLC",
                Name = "Baseboard Location in Chassis",
                Description = "The location string describing where the baseboard is installed in the chassis.",
                DefaultSpoofedValue = "Default string"
            },
            ["/CM"] = new AmidewinKeyDescriptor
            {
                Key = "/CM",
                Name = "Chassis Manufacturer",
                Description = "The manufacturer name of the system chassis.",
                DefaultSpoofedValue = "Default string"
            },
            ["/CV"] = new AmidewinKeyDescriptor
            {
                Key = "/CV",
                Name = "Chassis Version",
                Description = "The version string for the system chassis.",
                DefaultSpoofedValue = "Default string"
            },
            ["/CS"] = new AmidewinKeyDescriptor
            {
                Key = "/CS",
                Name = "Chassis Serial Number",
                Description = "The serial number assigned to the chassis by the manufacturer.",
                DefaultSpoofedValue = "Default string"
            },
            ["/CA"] = new AmidewinKeyDescriptor
            {
                Key = "/CA",
                Name = "Chassis Tag Number",
                Description = "The asset or tag number assigned to the chassis.",
                DefaultSpoofedValue = "Default string"
            },
            ["/CSK"] = new AmidewinKeyDescriptor
            {
                Key = "/CSK",
                Name = "Chassis SKU Number",
                Description = "The SKU or stock keeping unit number of the chassis.",
                DefaultSpoofedValue = "Default string"
            },
            ["/PSN"] = new AmidewinKeyDescriptor
            {
                Key = "/PSN",
                Name = "Processor Serial Number",
                Description = "The serial number assigned to the processor by the manufacturer.",
                DefaultSpoofedValue = "Unknown"
            },
            ["/PAT"] = new AmidewinKeyDescriptor
            {
                Key = "/PAT",
                Name = "Processor Asset Tag",
                Description = "The asset tag associated with the processor.",
                DefaultSpoofedValue = "Default string"
            },
            ["/PPN"] = new AmidewinKeyDescriptor
            {
                Key = "/PPN",
                Name = "Processor Part Number",
                Description = "The part number of the processor.",
                DefaultSpoofedValue = "Default string"
            },
            ["/OS"] = new AmidewinKeyDescriptor
            {
                Key = "/OS",
                Name = "OEM String #1",
                Description = "An OEM-defined string field.",
                DefaultSpoofedValue = "Default string"
            },
            ["/SCO"] = new AmidewinKeyDescriptor
            {
                Key = "/SCO",
                Name = "System Configuration Option #1",
                Description = "A system configuration option field defined by the OEM.",
                DefaultSpoofedValue = "Default string"
            },
            ["/PL"] = new AmidewinKeyDescriptor
            {
                Key = "/PL",
                Name = "Power Location",
                Description = "The location of the system power supply.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            },
            ["/PD"] = new AmidewinKeyDescriptor
            {
                Key = "/PD",
                Name = "Power Device Name",
                Description = "The name or identifier of the power supply device.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            },
            ["/PM"] = new AmidewinKeyDescriptor
            {
                Key = "/PM",
                Name = "Power Manufacturer",
                Description = "The manufacturer of the power supply.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            },
            ["/PS"] = new AmidewinKeyDescriptor
            {
                Key = "/PS",
                Name = "Power Serial Number",
                Description = "The serial number assigned to the power supply.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            },
            ["/PT"] = new AmidewinKeyDescriptor
            {
                Key = "/PT",
                Name = "Power Asset Tag",
                Description = "The asset tag number of the power supply.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            },
            ["/PN"] = new AmidewinKeyDescriptor
            {
                Key = "/PN",
                Name = "Power Model / Part Number",
                Description = "The model or part number of the power supply.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            },
            ["/PR"] = new AmidewinKeyDescriptor
            {
                Key = "/PR",
                Name = "Power Revision Level",
                Description = "The revision level of the power supply.",
                DefaultSpoofedValue = "To Be Filled By O.E.M."
            }
        };


        public static void Init()
        {
            try
            {
                if (!File.Exists(AMIDEWIN_EXE))
                    UnpackResource(AMIDEWIN_EXE);
                if (!File.Exists(AMIDEWIN_DRIVER))
                    UnpackResource(AMIDEWIN_DRIVER);
                CloseAll();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize AMIDEWIN", ex);
            }
        }

        private static void CloseAll()
        {
            var existing = Process.GetProcessesByName(AMIDEWIN_EXE.Split('.')[0]);
            foreach (var zombie in existing)
                zombie.Kill();
        }

        /// <summary>
        /// Gets the current value for the specified AMIDEWIN key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static AmidewinKeyValuePair GetValue(string key)
        {
            var raw = Invoke(key);
            return AmidewinParser.Parse(raw);
        }

        /// <summary>
        /// Applies the specified <see cref="AmidewinKeyValuePair.SpoofedValue"/> key to SMBIOS.
        /// </summary>
        /// <param name="value"></param>
        public static void SetValue(AmidewinKeyValuePair value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value.SpoofedValue, nameof(value.SpoofedValue));
            if (!value.HasChanged)
            {
                AnsiConsole.MarkupLine($"[grey]{value.Key} unchanged, skipping.[/]");
            }
            else if (value.SpoofedValue.Equals("AUTO", StringComparison.OrdinalIgnoreCase))
            {
                AnsiConsole.WriteLine(Invoke($"{value.Key} AUTO"));
            }
            else
            {
                AnsiConsole.WriteLine(Invoke($"{value.Key} \"{value.SpoofedValue}\""));
            }
        }

        private static string Invoke(string args)
        {
            using var proc = new Process()
            {
                StartInfo = new()
                {
                    FileName = AMIDEWIN_EXE,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            if (!proc.Start())
                throw new InvalidOperationException($"Failed to start {AMIDEWIN_EXE}");
            if (!proc.WaitForExit(timeout: TimeSpan.FromSeconds(10)))
                throw new InvalidOperationException($"Process {AMIDEWIN_EXE} timed out;\n{GetErrorOutput(proc)}");
            if (proc.ExitCode != 0)
                throw new InvalidOperationException($"Process {AMIDEWIN_EXE} exited with code {proc.ExitCode};\n{GetErrorOutput(proc)}");
            return proc.StandardOutput.ReadToEnd();
        }

        private static string GetErrorOutput(Process proc)
        {
            var sb = new StringBuilder();
            string stdout = proc.StandardOutput.ReadToEnd();
            string stderr = proc.StandardError.ReadToEnd();
            if (string.IsNullOrWhiteSpace(stdout))
                stdout = "<Empty>";
            if (string.IsNullOrWhiteSpace(stderr))
                stderr = "<Empty>";
            sb.AppendLine($"stdout: {stdout}");
            sb.AppendLine($"stderr: {stderr}");
            return sb.ToString();
        }

        private static void UnpackResource(string resource)
        {
            const string resourcePrefix = "LoneSpoof.Resources.";
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePrefix + resource) ??
                throw new InvalidOperationException($"Resource '{resource}' not found.");
            using var fsOut = new FileStream(
                path: resource,
                mode: FileMode.Create,
                access: FileAccess.Write);
            stream.CopyTo(destination: fsOut);
        }
    }
}
