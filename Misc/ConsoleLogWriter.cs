using System.Text;

namespace LoneSpoof.Misc
{
    public sealed class ConsoleLogWriter : TextWriter
    {
        private readonly TextWriter _consoleWriter;
        private readonly StreamWriter _fileWriter;
        private readonly Lock _sync = new();

        public ConsoleLogWriter(string path)
        {
            _consoleWriter = Console.Out;
            _fileWriter = new StreamWriter(path, true, Encoding.UTF8)
            {
                AutoFlush = true
            };
            Console.SetOut(this);
        }

        public override Encoding Encoding => _consoleWriter.Encoding;

        public override void Write(char value)
        {
            lock (_sync)
            {
                _consoleWriter.Write(value);
                _fileWriter.Write(value);
            }
        }

        public override void Write(string value)
        {
            lock (_sync)
            {
                _consoleWriter.Write(value);
                _fileWriter.Write(value);
            }
        }

        public override void WriteLine(string value)
        {
            lock (_sync)
            {
                _consoleWriter.WriteLine(value);
                _fileWriter.WriteLine(value);
            }
        }

        public override void Flush()
        {
            lock (_sync)
            {
                _consoleWriter.Flush();
                _fileWriter.Flush();
            }
        }

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            lock (_sync)
            {
                if (!_disposed)
                {
                    Console.SetOut(Console.Out);
                    if (disposing)
                    {
                        _fileWriter.Flush();
                        _fileWriter.Dispose();
                    }
                    base.Dispose(disposing);
                    _disposed = true;
                }
            }
        }
    }
}
