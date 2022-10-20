using desafio.feiras.infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.tests
{
    public class DesafioFileWriterMock : IDesafioFileWriter
    {
        public DesafioFileWriterMock()
        {
            this.Lines = new List<string>();
        }

        public List<string> Lines { get; private set; }

        public void AppendLines(string filepath, string[] lines)
        {
            this.Lines.AddRange(lines);
        }
    }
}
