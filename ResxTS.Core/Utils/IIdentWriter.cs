using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Utils
{
    public interface IIdentWriter
    {
        void WL(int i, string txt, params string[] parameters);
        void W(int i, string txt, params string[] parameters);
        void LineBreak();
    }
}
