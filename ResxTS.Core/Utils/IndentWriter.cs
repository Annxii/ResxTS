﻿using System;
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
            writer = new StreamWriter(stream, Encoding.UTF8);
        }

        public void WL(int i, string txt, params string[] parameters)
        {
            W(i, txt, parameters);
            writer.WriteLine();
        }

        public void W(int i, string txt, params string[] parameters)
        {
            writer.Write(new string(' ', i * 4));
            writer.Write(txt, parameters);
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
