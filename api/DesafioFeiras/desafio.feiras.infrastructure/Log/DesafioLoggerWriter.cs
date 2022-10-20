using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.Log
{
    public interface IDesafioLoggerWriter
    {
        void Write(DesafioLogStructure log);
    }

    public abstract class DesafioLoggerWriterBase : IDesafioLoggerWriter
    {
        private static Regex regexFilename = new Regex(@"\{(?<Key>[0-9a-zA-Z]+)\:?(?<Format>[^}]+)?\}");

        private readonly DesafioLoggerSettings settings;
        private readonly IDesafioFileWriter fileWriter;
        private readonly string baseLoggerDirectory;

        public DesafioLoggerWriterBase(DesafioLoggerSettings settings, IDesafioFileWriter fileWriter)
        {
            this.settings = settings;
            this.fileWriter = fileWriter;
            this.baseLoggerDirectory = GetLogDirectory();
        }

        [ExcludeFromCodeCoverage]
        private string GetLogDirectory()
        {
            var directory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"));

            if (!directory.Exists)
                directory.Create();

            return directory.FullName;
        }

        public virtual void Write(DesafioLogStructure log)
        {
            string filename = GenerateFilename();

            fileWriter.AppendLines(Path.Combine(baseLoggerDirectory, filename), FormatLog(log));
        }

        protected abstract string[] FormatLog(DesafioLogStructure log);

        protected virtual string GenerateFilename()
        {
            var data = new { Application = settings.Application, Date = DateTime.Now };

            return regexFilename.Replace(settings.FilePattern, (m) =>
            {
                var key = m.Groups["Key"].Value;
                var property = data.GetType().GetProperty(key);
                var value = property?.GetValue(data);

                if (property?.PropertyType == typeof(DateTime))
                {
                    var format = m.Groups["Format"].Value;
                    return Convert.ToDateTime(value).ToString(format);
                }
                else
                    return value?.ToString() ?? "";
            });
        }
    }
   
}
