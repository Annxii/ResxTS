using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Scanner
{
    public interface IResxValueScanner<T> where T : ResxValue
    {
        T Scan(string text);
    }
}
