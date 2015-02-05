using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Utils
{
    public class TypeScriptBuilder : IIdentWriter
    {
        private readonly StringBuilder b = new StringBuilder();

        public void WL(int i, string txt, params string[] parameters)
        {
            W(i, txt, parameters);
            b.AppendLine();
        }

        public void W(int i, string txt, params string[] parameters)
        {
            var indent = new string(' ', i * 4);
            if (parameters.Length > 0)
                b.AppendFormat(indent + txt, parameters);
            else
                b.Append(indent + txt);
        }

        public void LineBreak()
        {
            b.AppendLine();
        }

        public override string ToString()
        {
            return b.ToString();
        }
    }
}
