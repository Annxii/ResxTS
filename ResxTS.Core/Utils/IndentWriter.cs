using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Utils
{
    public class IndentWriter : IIdentWriter, IDisposable
    {
        private StreamWriter writer;

        public IndentWriter(Stream stream)
        {
            writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
        }

        public void WL(int i, string txt, params string[] parameters)
        {
            W(i, txt, parameters);
            writer.WriteLine();
        }

        public void W(int i, string txt, params string[] parameters)
        {
            writer.Write(new string(' ', i * 4));
            if (parameters != null && parameters.Length > 0)
                writer.Write(txt, parameters);
            else
                writer.Write(txt);
        }

        public void LineBreak()
        {
            writer.WriteLine();
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
