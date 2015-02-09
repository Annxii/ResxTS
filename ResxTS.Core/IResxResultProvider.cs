using ResxTS.Core.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core
{
    public interface IResxResultProvider<TResult> where TResult : ResxResult
    {
        TResult GetResult(string filePath);
    }
}
